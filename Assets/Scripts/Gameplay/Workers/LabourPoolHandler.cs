using System.Collections.Generic;
using UnityEngine;

public class LabourPoolHandler
{
    public static List<IWorker> AddWorkerToLabourPool(List<IWorker> workers, IWorkerLocation location)
    {
        workers.Add(new ResourcesWorker(location));
        return workers;
    }

    public static List<IWorker> AddCityWorkerToLabourPool(List<IWorker> workers, IWorkerLocation location)
    {
        workers.Add(new CityWorker(location));
        return workers;
    }

    public static List<IWorker> RemoveWorkerFromLabourPool(List<IWorker> workers, IWorker worker)
    {
        bool wasRemoved = workers.Remove(worker);
        if (!wasRemoved)
        {
            Debug.LogWarning($"Worker was NOT removed");
        }

        return workers;
    }

    public static List<IWorker> RemoveCityWorkerFromLabourPool(List<IWorker> workers, IWorker worker)
    {
        bool wasRemoved = workers.Remove(worker);
        if (!wasRemoved)
        {
            Debug.LogWarning($"Worker was NOT removed");

            Debug.Log($"{workers[0].UIWorkerTile.gameObject.name}");
        }

        return workers;
    }
}