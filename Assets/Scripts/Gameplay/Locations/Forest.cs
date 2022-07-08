using System.Collections.Generic;
using UnityEngine;

public class Forest : IPlayerLocation, IWorkerLocation, ILabourPoolLocation
{
    public ResourceType ResourceType { get; private set; } = ResourceType.Wood;
    public LocationType LocationType { get; private set; } = LocationType.Forest;
    public string Name { get; private set; } = "Forest";
    private List<IWorker> _labourPoolWorkers = new List<IWorker>();

    public void AddWorkerToLabourPool()
    {
        _labourPoolWorkers = LabourPoolHandler.AddWorkerToLabourPool(_labourPoolWorkers, this);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public void RemoveWorkerFromLabourPool(IWorker worker)
    {
        _labourPoolWorkers = LabourPoolHandler.RemoveWorkerFromLabourPool(_labourPoolWorkers, worker);
        LocationManager.Instance.UpdateLabourPoolLocationUI(LocationType);
    }

    public GameObject GetWorkerPrefabForLocation()
    {
        return AssetManager.Instance.ResourcesWorkerPrefab;
    }

    public List<IWorker> GetLabourPoolWorkers()
    {
        return _labourPoolWorkers;
    }
}
