using System.Collections.Generic;

public class PickTravelLocationStep : IGameActionStep, IUILocationSelectionGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private ILocation _selectedLocation;

    private Dictionary<LocationType, GameActionLocationSelectionTileElement> _locationTileByLocationType = new Dictionary<LocationType, GameActionLocationSelectionTileElement>();

    public PickTravelLocationStep()
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

        AddPossibleLocations();

        // by default select first available tile
        foreach (KeyValuePair<LocationType, GameActionLocationSelectionTileElement> item in _locationTileByLocationType)
        {
            if (item.Value.IsAvailable)
            {
                _locationTileByLocationType[item.Key].Select();
                _selectedLocation = item.Value.TargetLocation;
                return _elements;
            }
        }

        return _elements;
    }

    private void AddPossibleLocations()
    {
        Player player = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

        // iterate over all player locations
        Dictionary<LocationType, IPlayerLocation> playerLocations = LocationManager.Instance.GetPlayerLocations();

        foreach (KeyValuePair<LocationType, IPlayerLocation> item in playerLocations)
        {
            AddTargetLocationElement(player, item.Value);
        }
    }

    public void SelectLocation(LocationType locationType)
    {
        if (_selectedLocation.LocationType == locationType) return;

        LocationType previouslySelectedLocationType = _selectedLocation.LocationType;
        _locationTileByLocationType[previouslySelectedLocationType].Deselect();

        _selectedLocation = LocationManager.Instance.GetLocation(locationType);
        _locationTileByLocationType[_selectedLocation.LocationType].Select();
    }


    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithLocation(_selectedLocation);
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    private void AddTargetLocationElement(Player player, ILocation location)
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
