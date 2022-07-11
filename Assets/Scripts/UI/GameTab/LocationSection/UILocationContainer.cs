using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocationContainer : MonoBehaviour, ILocationUIContainer
{
    [SerializeField] private TextMeshProUGUI _subTitleTextField;
    [SerializeField] private Button _shrinkLabourPoolButton;
    [SerializeField] private Button _growLabourPoolButton;
    private LocationType _locationType;

    [SerializeField] private Transform _workersContainer;
    public List<WorkerTile> WorkerTiles { get; private set; } = new List<WorkerTile>();

    [SerializeField] private Image[] _playerIconSlot;
    Dictionary<Player, Image> _usedPlayerIcons = new Dictionary<Player, Image>();


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
        if (GameActionStepHandler.CurrentGameActionSequence != null) return;

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
        if (GameActionStepHandler.CurrentGameActionSequence != null) return;

        ILabourPoolLocation location = LocationManager.Instance.GetLabourPoolLocation(_locationType);

        WorkerTile workerTile = AddWorkerTile(location.LocationType);
        location.AddWorkerToLabourPool();
        List<IWorker> labourpoolWorkers = location.GetLabourPoolWorkers();
        workerTile.Initialise(_locationType, labourpoolWorkers[labourpoolWorkers.Count - 1]);
    }

    public WorkerTile AddWorkerTile(LocationType locationType)
    {
        IWorkerLocation location = LocationManager.Instance.GetWorkerLocation(_locationType);

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

    public void AddPlayerToLocation(Player player)
    {
        Debug.Log($"add {player.Name} to {_locationType} location");
        int numberOfUsedPlayerIcons = _usedPlayerIcons.Count;

        _usedPlayerIcons.Add(player, _playerIconSlot[numberOfUsedPlayerIcons]);
        _playerIconSlot[numberOfUsedPlayerIcons].sprite = player.Avatar;
        _playerIconSlot[numberOfUsedPlayerIcons].enabled = true;

    }

    public void RemovePlayerFromLocation(Player player)
    {
        Debug.Log($"remove {player.Name} from {_locationType} location");
        _playerIconSlot[_usedPlayerIcons.Count - 1].sprite = null;
        _playerIconSlot[_usedPlayerIcons.Count - 1].enabled = false;

        if (_usedPlayerIcons.TryGetValue(player, out Image image))
        {
            _usedPlayerIcons.Remove(player);
        }

        Dictionary<Player, Image> _updatedUsedPlayerIcons = new Dictionary<Player, Image>();
        foreach (KeyValuePair<Player, Image> item in _usedPlayerIcons)
        {
            if (item.Key == player) continue;

            _updatedUsedPlayerIcons.Add(item.Key, item.Value);
            _playerIconSlot[_updatedUsedPlayerIcons.Count - 1].sprite = item.Key.Avatar;
            _playerIconSlot[_updatedUsedPlayerIcons.Count - 1].enabled = true;
        }
    }
}
