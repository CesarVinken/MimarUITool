using System.Collections.Generic;

public class PickHiringLocationStep : IGameActionStep, IUILocationSelectionGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private ILocation _selectedLocation;

    private Dictionary<LocationType, GameActionLocationSelectionTileElement> _locationTileByLocationType = new Dictionary<LocationType, GameActionLocationSelectionTileElement>();

    public PickHiringLocationStep()
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

        Dictionary<LocationType, IWorkerLocation> workerLocations = LocationManager.Instance.GetWorkerLocations();
        foreach (KeyValuePair<LocationType, IWorkerLocation> item in workerLocations)
        {
            // Only show the construction site of the player
            if (item.Value.ResourceType == ResourceType.LabourTime && item.Key != player.Monument.ConstructionSite) continue;

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
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new PickWorkerGameActionStep());

        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithLocation(_selectedLocation);
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    private void AddTargetLocationElement(Player player, IWorkerLocation workerLocation)
    {
        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(workerLocation.LocationType);

        GameActionLocationSelectionTileElement locationSelectionTileElement = GameActionElementInitialiser.InitialiseLocationSelectionTile(this, workerLocation);

        List<IWorker> labourPoolWorkers = labourPoolLocation.GetLabourPoolWorkers();
        if (labourPoolWorkers.Count == 0)
        {
            locationSelectionTileElement.MakeUnavailable();
        }

        _locationTileByLocationType.Add(workerLocation.LocationType, locationSelectionTileElement);
        _elements.Add(locationSelectionTileElement);
    }
}
