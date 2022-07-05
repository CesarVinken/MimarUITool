using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocationContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _subTitleTextField;
    [SerializeField] private Button _shrinkLabourPoolButton;
    [SerializeField] private Button _growLabourPoolButton;
    private LocationType _locationType;

    [SerializeField] private Transform _workersContainer;
    public List<WorkerTile> WorkerTiles { get; private set; } = new List<WorkerTile>();

    private void Awake()
    {
        if (_subTitleTextField == null)
        {
            Debug.LogError($"Could not find subtitle text field on {gameObject.name}");
        }
        if (_shrinkLabourPoolButton == null)
        {
            Debug.LogError($"Could not find shrinkLabourPoolButton on {gameObject.name}");
        }
        if (_growLabourPoolButton == null)
        {
            Debug.LogError($"Could not find growLabourPoolButton on {gameObject.name}");
        }

        if (_workersContainer == null)
        {
            Debug.LogError($"Could not find workersContainer on {gameObject.name}");
        }

        _shrinkLabourPoolButton.onClick.AddListener(() =>
        {
            ShrinkLabourPool();
        });
        _growLabourPoolButton.onClick.AddListener(() =>
        {
            GrowLabourPool();
        });
    }

    public void SetLocationType(LocationType locationType)
    {
        _locationType = locationType;
    }

    public void SetSubTitleText(int numberOfWorkers)
    {
        _subTitleTextField.text = $"Labour pool: {numberOfWorkers}";
    }

    public void ShrinkLabourPool()
    {
        if (GameActionHandler.CurrentUIGameToolAction != null) return;

        if (WorkerTiles.Count <= 0) return;

        ILocation location = LocationManager.Instance.GetLocation(_locationType);
        ILabourPoolLocation resourcesLocation = LocationManager.Instance.GetLabourPoolLocation(_locationType);

        WorkerTile workerTileToRemove = IdentifyWorkerTileToRemove(location);
        if (workerTileToRemove == null)
        {
            Debug.LogError($"Worker tile to remove is null");
            return;
        }

        resourcesLocation.RemoveWorkerFromLabourPool(workerTileToRemove.Worker);
        
        RemoveWorkerTile(workerTileToRemove);
    }

    private WorkerTile IdentifyWorkerTileToRemove(ILocation location)
    {
        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(location.LocationType);

        if(WorkerTiles.Count == 0)
        {
            Debug.Log($"no unemployed workers found. Return null");
            return null;
        }

        // The last neutral worker, which should get removed
        WorkerTile unemployedWorkerTile = WorkerTiles.FirstOrDefault(
             t => t.Worker.Employer == PlayerNumber.None);

        if(unemployedWorkerTile == null)
        {
            WorkerTile lastEmployedWorkerTile = WorkerTiles[WorkerTiles.Count - 1];
            return lastEmployedWorkerTile;
        }

        return unemployedWorkerTile;
    }

    public void RemoveWorkerTile(WorkerTile workerTileToRemove)
    {
        if (workerTileToRemove == null) return;

        WorkerTiles.Remove(workerTileToRemove);
        workerTileToRemove.Destroy();
    }

    public List<WorkerTile> GetWorkerTiles()
    {
        return WorkerTiles;
    }

    public void GrowLabourPool()
    {
        if (GameActionHandler.CurrentUIGameToolAction != null) return;

        ILocation location = LocationManager.Instance.GetLocation(_locationType);
        ILabourPoolLocation resourcesLocation = location as ILabourPoolLocation;

        if (resourcesLocation == null)
        {
            Debug.LogError($"Could not parse {location.Name} as a IResourceLocation");
            return;
        }

        WorkerTile workerTile = AddWorkerTile(resourcesLocation);
        resourcesLocation.AddWorkerToLabourPool();
        workerTile.Initialise(_locationType, resourcesLocation.LabourPoolWorkers[resourcesLocation.LabourPoolWorkers.Count - 1]);
    }

    public WorkerTile AddWorkerTile(ILabourPoolLocation location)
    {
        GameObject workerPrefab = location.GetWorkerPrefabForLocation();
        GameObject workerGO = GameObject.Instantiate(workerPrefab, _workersContainer);
        WorkerTile workerTile = workerGO.GetComponent<WorkerTile>();

        if (workerTile == null)
        {
            Debug.LogError($"Could not parse worker as a ResourcesWorkerTile");
            return null;
        }

        WorkerTiles.Add(workerTile);
        return workerTile;
    }
}
