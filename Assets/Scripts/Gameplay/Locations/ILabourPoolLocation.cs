using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILabourPoolLocation
{
    public List<IWorker> LabourPoolWorkers { get; }
    public GameObject GetWorkerPrefabForLocation();

    public void AddWorkerToLabourPool();
    public void RemoveWorkerFromLabourPool(IWorker worker);


}
