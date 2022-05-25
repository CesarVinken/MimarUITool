using System.Collections.Generic;
using UnityEngine;

public class Forest : ILocation, ILabourPoolLocation
{
    public LocationType LocationType { get; private set; } = LocationType.Forest;
    public string Name { get; private set; } = "Forest";
    public List<IWorker> LabourPoolWorkers { get; private set; }  = new List<IWorker>();

    public void AddWorkerToLabourPool()
    {
        LabourPoolWorkers = LabourPoolHandler.AddWorkerToLabourPool(LabourPoolWorkers, this);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public void RemoveWorkerFromLabourPool(IWorker worker)
    {
        LabourPoolWorkers = LabourPoolHandler.RemoveWorkerFromLabourPool(LabourPoolWorkers, worker);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public GameObject GetWorkerPrefabForLocation()
    {
        return AssetManager.Instance.ResourcesWorkerPrefab;
    }
}
