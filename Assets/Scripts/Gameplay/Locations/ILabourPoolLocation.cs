using System.Collections.Generic;
using UnityEngine;

public interface ILabourPoolLocation : ILocation
{
    public List<IWorker> GetLabourPoolWorkers();
    public void AddWorkerToLabourPool();
    public void RemoveWorkerFromLabourPool(IWorker worker);
}
