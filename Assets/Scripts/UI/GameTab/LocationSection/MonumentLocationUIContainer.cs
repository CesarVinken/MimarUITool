using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonumentLocationUIContainer : MonoBehaviour
{
    [SerializeField] private UILocationContainer _constantinopleContainer;

    [SerializeField] private Button _addWorkerButton;
    [SerializeField] private Button _removeWorkerButton;

    [SerializeField] private Transform _workersContainer;
    private List<WorkerTile> _workerTiles = new List<WorkerTile>();
    private LocationType _locationType;

    private void Awake()
    {
        if(_constantinopleContainer == null)
        {
            Debug.LogError($"Could not find constantinople container on {gameObject.name}");
        }
        if (_workersContainer == null)
        {
            Debug.LogError($"Could not find _workersContainer on {gameObject.name}");
        }
        if (_addWorkerButton == null)
        {
            Debug.LogError($"Could not find _addWorkerButton on {gameObject.name}");
        }
        if (_removeWorkerButton == null)
        {
            Debug.LogError($"Could not find _removeWorkerButton on {gameObject.name}");
        }

        _removeWorkerButton.onClick.AddListener(() =>
        {
            RemoveWorker();
        });
        _addWorkerButton.onClick.AddListener(() =>
        {
            AddWorker();
        });
    }

    public void SetLocationType(LocationType locationType)
    {
        _locationType = locationType;
    }

    // Remove worker from construction site, add worker as neutral worker
    public void RemoveWorker()
    {
        if (_workerTiles.Count <= 0) return;

        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(LocationType.Constantinople);

        WorkerTile lastWorkerTile = _workerTiles[_workerTiles.Count - 1];
        IWorker transferredWorker = lastWorkerTile.Worker;

        WorkerTile constantinopleWorkerTile = _constantinopleContainer.AddWorkerTile(labourPoolLocation);

        constantinopleWorkerTile.Initialise(LocationType.Constantinople, transferredWorker);

        if (lastWorkerTile == null)
        {
            Debug.LogError($"Could not find WorkerTile");
        }

        _workerTiles.Remove(lastWorkerTile);

        lastWorkerTile.Destroy();
    }

    // Add worker tile to construction site, remove neutral worker tile work Constantinople location
    public void AddWorker()
    {
        List<WorkerTile> constantinopleWorkerTiles = _constantinopleContainer.GetWorkerTiles();
        if (constantinopleWorkerTiles.Count < 1) return; // There should be neutral workers available in the shared worker pool

        ILocation buildingSiteLocation = LocationManager.Instance.GetLocation(_locationType);
        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(LocationType.Constantinople);

        // Remove worker tile from Constantinople
        WorkerTile lastNeutralWorkerTile = constantinopleWorkerTiles[constantinopleWorkerTiles.Count - 1];
        LabourPoolHandler.RemoveCityWorkerFromLabourPool(labourPoolLocation.LabourPoolWorkers, lastNeutralWorkerTile.Worker);

        // Add worker tile to construction site
        GameObject workerPrefab = labourPoolLocation.GetWorkerPrefabForLocation();
        GameObject workerGO = GameObject.Instantiate(workerPrefab, _workersContainer);
        CityWorkerTile workerTile = workerGO.GetComponent<CityWorkerTile>();

        if (workerTile == null)
        {
            Debug.LogError($"Could not parse worker as a CityWorkerTile");
            return;
        }

        List<IWorker> updatedLabourPool = LabourPoolHandler.AddCityWorkerToLabourPool(labourPoolLocation.LabourPoolWorkers, buildingSiteLocation);
        workerTile.Worker = updatedLabourPool[updatedLabourPool.Count - 1];
        workerTile.Initialise(_locationType, workerTile.Worker);
        _workerTiles.Add(workerTile);
        _constantinopleContainer.RemoveWorkerTile(lastNeutralWorkerTile);
    }
}
