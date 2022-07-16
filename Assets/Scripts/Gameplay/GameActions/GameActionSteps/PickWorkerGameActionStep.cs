using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWorkerGameActionStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private IWorker _selectedWorker;

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

    public void SelectWorker()
    {
        Debug.Log($"select worker");
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithWorker(_selectedWorker);
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

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
        //Player player = PlayerManager.Instance.Players[worker.Employer];


        //ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(workerLocation.LocationType);

        GameActionWorkerSelectionTileElement workerSelectionTileElement = GameActionElementInitialiser.InitialiseWorkerSelectionTile(this, worker);

        //List<IWorker> labourPoolWorkers = labourPoolLocation.GetLabourPoolWorkers();
        //if (labourPoolWorkers.Count == 0)
        //{
        //    locationSelectionTileElement.MakeUnavailable();
        //}

        //_locationTileByLocationType.Add(workerLocation.LocationType, locationSelectionTileElement);
        _elements.Add(workerSelectionTileElement);
    }
}
