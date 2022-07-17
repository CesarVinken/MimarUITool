using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonumentLocationUIContainer : MonoBehaviour, ILocationUIContainer
{
    [SerializeField] private UILocationContainer _romeContainer;

    [SerializeField] private Button _addWorkerButton;
    [SerializeField] private Button _removeWorkerButton;

    [SerializeField] private Transform _workersContainer;
    private List<WorkerTile> _workerTiles = new List<WorkerTile>();
    private LocationType _locationType;

    [SerializeField] private Image[] _playerIconSlot;
    [SerializeField] private TextMeshProUGUI _locationName;
    Dictionary<Player, Image> _usedPlayerIcons = new Dictionary<Player, Image>();

    private void Awake()
    {
        if(_romeContainer == null)
        {
            Debug.LogError($"Could not find Rome container on {gameObject.name}");
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
        if (_locationName == null)
        {
            Debug.LogError($"Could not find _locationName on {gameObject.name}");
        }
        if (_playerIconSlot.Length != 3)
        {
            Debug.LogError($"There should be 3 slots for player icons on {gameObject.name}");
        }

        _removeWorkerButton.onClick.AddListener(() =>
        {
            RemoveLastWorkerFromSite();
        });
        _addWorkerButton.onClick.AddListener(() =>
        {
            AddWorker();
        });
    }

    public void Initialise()
    {
        if (_locationType == LocationType.ConstructionSite1)
        {
            _locationName.text = $"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player1])} Site";
        }
        else if (_locationType == LocationType.ConstructionSite2)
        {
            _locationName.text = $"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player2])} Site";
        }
        else if (_locationType == LocationType.ConstructionSite3)
        {
            _locationName.text = $"{PlayerUtility.GetPossessivePlayerString(PlayerManager.Instance.Players[PlayerNumber.Player3])} Site";
        }
    }

    public void SetLocationType(LocationType locationType)
    {
        _locationType = locationType;
    }

    // Remove last worker from construction site, add worker as neutral worker
    public void RemoveLastWorkerFromSite()
    {
        if (_workerTiles.Count <= 0) return;

        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(LocationType.Rome);

        WorkerTile lastWorkerTile = _workerTiles[_workerTiles.Count - 1];
        IWorker transferredWorker = lastWorkerTile.Worker;

        WorkerTile romeWorkerTile = _romeContainer.AddWorkerTile(labourPoolLocation.LocationType);

        romeWorkerTile.Initialise(LocationType.Rome, transferredWorker);

        if (lastWorkerTile == null)
        {
            Debug.LogError($"Could not find WorkerTile");
        }

        _workerTiles.Remove(lastWorkerTile);

        lastWorkerTile.Destroy();
    }

    // remove a particular worker from a site (for example, when a contract finishes. Make a worker available in the neutral pool
    public void RemoveWorkerFromSite(WorkerTile workerTile)
    {
        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(LocationType.Rome);

        IWorker transferredWorker = workerTile.Worker;

        WorkerTile romeWorkerTile = _romeContainer.AddWorkerTile(labourPoolLocation.LocationType);

        romeWorkerTile.Initialise(LocationType.Rome, transferredWorker);

        if (workerTile == null)
        {
            Debug.LogError($"Could not find WorkerTile");
        }

        _workerTiles.Remove(workerTile);

        workerTile.Destroy();
    }

    // Add worker tile to construction site, remove neutral worker tile work Rome location
    public void AddWorker()
    {
        List<WorkerTile> romeWorkerTiles = _romeContainer.GetWorkerTiles();
        if (romeWorkerTiles.Count < 1) return; // There should be neutral workers available in the shared worker pool

        IWorkerLocation buildingSiteLocation = LocationManager.Instance.GetWorkerLocation(_locationType);

        // Remove worker tile from Rome
        WorkerTile lastNeutralWorkerTile = romeWorkerTiles[romeWorkerTiles.Count - 1];
        LabourPoolHandler.RemoveCityWorkerFromLabourPool(Rome.LabourPoolWorkers, lastNeutralWorkerTile.Worker);

        // Add worker tile to construction site
        GameObject workerPrefab = buildingSiteLocation.GetWorkerPrefabForLocation();
        GameObject workerGO = GameObject.Instantiate(workerPrefab, _workersContainer);
        CityWorkerTile workerTile = workerGO.GetComponent<CityWorkerTile>();

        if (workerTile == null)
        {
            Debug.LogError($"Could not parse worker as a CityWorkerTile");
            return;
        }

        List<IWorker> updatedLabourPool = LabourPoolHandler.AddCityWorkerToLabourPool(Rome.LabourPoolWorkers, buildingSiteLocation);
        workerTile.Worker = updatedLabourPool[updatedLabourPool.Count - 1];
        workerTile.Initialise(_locationType, workerTile.Worker);
        _workerTiles.Add(workerTile);
        _romeContainer.RemoveWorkerTile(lastNeutralWorkerTile);
    }

    public void AddPlayerToLocation(Player player)
    {
        int numberOfUsedPlayerIcons = _usedPlayerIcons.Count;

        _usedPlayerIcons.Add(player, _playerIconSlot[numberOfUsedPlayerIcons]);
        _playerIconSlot[numberOfUsedPlayerIcons].sprite = player.Avatar;
        _playerIconSlot[numberOfUsedPlayerIcons].enabled = true;
    }

    public void RemovePlayerFromLocation(Player player)
    {
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
