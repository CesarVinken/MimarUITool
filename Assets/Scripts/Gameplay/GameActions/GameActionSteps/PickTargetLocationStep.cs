using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickTargetLocationStep : IGameActionStep, IUILocationSelectionGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private LocationType _selectedLocationType;

    private Dictionary<LocationType, GameActionLocationSelectionTileElement> _locationTileByLocationType = new Dictionary<LocationType, GameActionLocationSelectionTileElement>();

    public PickTargetLocationStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IGameActionElement> Initialise()
    {
        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        Player travellingPlayer = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

        // iterate over all player locations
        Dictionary<LocationType, IPlayerLocation> playerLocations = LocationManager.Instance.GetPlayerLocations();

        foreach (KeyValuePair<LocationType, IPlayerLocation> item in playerLocations)
        {
            AddTargetLocationElement(travellingPlayer, item.Value);
        }

        // by default select first available tile
        foreach (KeyValuePair<LocationType, GameActionLocationSelectionTileElement> item in _locationTileByLocationType)
        {
            if (item.Value.IsAvailable)
            {
                _locationTileByLocationType[item.Key].Select();
                _selectedLocationType = item.Key;
                return _elements;
            }
        }

        return _elements;
    }

    public void SelectLocation(LocationType locationType)
    {
        if (_selectedLocationType == locationType) return;

        LocationType previouslySelectedLocationType = _selectedLocationType;
        _locationTileByLocationType[previouslySelectedLocationType].Deselect();

        _selectedLocationType = locationType;
        _locationTileByLocationType[_selectedLocationType].Select();
    }


    public void NextStep()
    {
        throw new System.NotImplementedException();
    }

    private void AddTargetLocationElement(Player player, IPlayerLocation location)
    {
        GameActionLocationSelectionTileElement locationSelectionTileElement = GameActionElementInitialiser.InitialiseLocationSelectionTile(this, location);
        
        if (player.Gold.Value == 0 || player.Location.LocationType == location.LocationType)
        {
            locationSelectionTileElement.MakeUnavailable();
        }

        _locationTileByLocationType.Add(location.LocationType, locationSelectionTileElement);
        _elements.Add(locationSelectionTileElement);
    }
}
