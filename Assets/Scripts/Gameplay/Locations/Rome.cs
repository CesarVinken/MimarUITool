using System.Collections.Generic;
using UnityEngine;

public class Rome : IWorkerLocation, ILabourPoolLocation
{
    public ResourceType ResourceType { get; private set; } = ResourceType.LabourTime;
    public LocationType LocationType { get; private set; } = LocationType.Rome;
    public string Name { get; private set; } = "Rome";
    public static List<IWorker> LabourPoolWorkers { get; private set; } = new List<IWorker>();

    public void AddWorkerToLabourPool()
    {
        LabourPoolWorkers = LabourPoolHandler.AddCityWorkerToLabourPool(LabourPoolWorkers, this);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public void RemoveWorkerFromLabourPool(IWorker worker)
    {
        LabourPoolWorkers = LabourPoolHandler.RemoveWorkerFromLabourPool(LabourPoolWorkers, worker);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public GameObject GetWorkerPrefabForLocation()
    {
        return AssetManager.Instance.CityWorkerPrefab;
    }

    public List<IWorker> GetLabourPoolWorkers()
    {
        return LabourPoolWorkers;
    }

}
