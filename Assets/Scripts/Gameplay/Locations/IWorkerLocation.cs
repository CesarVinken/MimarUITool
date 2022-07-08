using UnityEngine;

public interface IWorkerLocation : ILocation
{
    public ResourceType ResourceType { get; }
    public GameObject GetWorkerPrefabForLocation();
}
