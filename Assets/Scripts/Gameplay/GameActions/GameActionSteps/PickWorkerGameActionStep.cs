using System.Collections.Generic;
using UnityEngine;

public class PickWorkerGameActionStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private GameActionWorkerSelectionTileElement _selectedTileElement;
    List<GameActionWorkerSelectionTileElement> _workerSelectionTiles = new List<GameActionWorkerSelectionTileElement>();

    public PickWorkerGameActionStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> Initialise()
    {
        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        // add tiles
        AddWorkerTiles();

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        for (int i = 0; i < _workerSelectionTiles.Count; i++)
        {
            if (_workerSelectionTiles[i].IsAvailable)
            {
                _selectedTileElement = _workerSelectionTiles[i];
                _selectedTileElement.Select();
                return _elements;
            }
        }

        Debug.LogWarning($"We have no available workers. We need a return scenario for this.");
        return _elements;
    }

    private void AddWorkerTiles()
    {
        ILocation actionLocation = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Location;

        if (actionLocation == null)
        {
            Debug.LogError($"could not find location on checksum");
        }

        List<IWorker> usableWorkers = GetWorkersForTiles(actionLocation);

        for (int j = 0; j < usableWorkers.Count; j++)
        {
            AddWorkerElement(usableWorkers[j]);
        }
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public void SelectWorker(GameActionWorkerSelectionTileElement tile)
    {
        if(_selectedTileElement != null)
        {
            _selectedTileElement.Deselect();
        }

        _selectedTileElement = tile;
        _selectedTileElement.Select();
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithWorker(_selectedTileElement.Worker);
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());//TODO based on the worker situation, set follow up step

        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    private List<IWorker> GetWorkersForTiles(ILocation actionLocation)
    {
        LocationType locationType = actionLocation.LocationType;
        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(locationType);
        List<IWorker> labourPoolWorkers = labourPoolLocation.GetLabourPoolWorkers();
        List<IWorker> usableWorkers = new List<IWorker>();

        if (actionLocation is ConstructionSite) // only include neutral workers, or workers that are at the specified action location
        {
            for (int i = 0; i < labourPoolWorkers.Count; i++)
            {
                LocationType labourPoolWorkerLocation = labourPoolWorkers[i].Location.LocationType;
                if (labourPoolWorkerLocation == LocationType.Constantinople || labourPoolWorkerLocation == actionLocation.LocationType)
                {
                    usableWorkers.Add(labourPoolWorkers[i]);
                }
            }
        }
        else
        {
            usableWorkers.AddRange(labourPoolWorkers);
        }

        return usableWorkers;
    }

    private void AddWorkerElement(IWorker worker)
    {
        HireWorkerActionType hireWorkerActionType = DetermineWorkerActionType(worker);
        GameActionWorkerSelectionTileElement workerSelectionTileElement = GameActionElementInitialiser.InitialiseWorkerSelectionTile(this, worker, hireWorkerActionType);

        if (!WorkerActionPossible(workerSelectionTileElement, worker))
        {
            workerSelectionTileElement.MakeUnavailable();
        }
        _workerSelectionTiles.Add(workerSelectionTileElement);
        _elements.Add(workerSelectionTileElement);
    }

    private HireWorkerActionType DetermineWorkerActionType(IWorker worker)
    {
        Player actionInitiator = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;
        PlayerNumber employer = worker.Employer;

        if (actionInitiator.PlayerNumber == employer)
        {
            return HireWorkerActionType.ExtendContract;
        }
        if(employer == PlayerNumber.None)
        {
            return HireWorkerActionType.Hire;
        }
        return HireWorkerActionType.Bribe;
    }

    private bool WorkerActionPossible(GameActionWorkerSelectionTileElement workerSelectionTileElement, IWorker worker)
    {
        HireWorkerActionType hireWorkerActionType = workerSelectionTileElement.HireWorkerActionType;
        Player actionInitiator = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

        switch (hireWorkerActionType)
        {
            case HireWorkerActionType.Bribe:
                if(actionInitiator.Gold.Value < 8)
                {
                    return false;
                }
                break;
            case HireWorkerActionType.ExtendContract:
                if (actionInitiator.Gold.Value < 2)
                {
                    return false;
                }
                break;
            case HireWorkerActionType.Hire:
                if (actionInitiator.Gold.Value < 4)
                {
                    return false;
                }
                break;
            default:
                new NotImplementedException("hireWorkerActionType", hireWorkerActionType.ToString());
                break;
        }
        return true;
    }
}
