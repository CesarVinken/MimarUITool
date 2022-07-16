using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite : IPlayerLocation, IWorkerLocation
{
    public LocationType LocationType { get; private set; }
    public string Name { get; private set; } = "";

    public List<IWorker> LabourPoolWorkers { get; private set; } = new List<IWorker>();

    public ResourceType ResourceType { get; private set; } = ResourceType.LabourTime;
    private UILocationContainer _uiLocationContainer;

    public ConstructionSite(LocationType locationType, string name = "")
    {
        if (string.IsNullOrEmpty(name))
        {
            name = LocationType.ToString();
        }

        LocationType = locationType;
        Name = name;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public GameObject GetWorkerPrefabForLocation()
    {
        return AssetManager.Instance.CityWorkerPrefab;
    }
}
