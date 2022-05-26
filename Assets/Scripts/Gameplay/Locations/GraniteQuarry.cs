using System.Collections.Generic;
using UnityEngine;

public class GraniteQuarry : ILocation, ILabourPoolLocation
{
    public ResourceType ResourceType { get; private set; } = ResourceType.Granite;
    public LocationType LocationType { get; private set; } = LocationType.GraniteQuarry;
    public string Name { get; private set; } = "Granite Quarry";
    public List<IWorker> LabourPoolWorkers { get; private set; } = new List<IWorker>();

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
