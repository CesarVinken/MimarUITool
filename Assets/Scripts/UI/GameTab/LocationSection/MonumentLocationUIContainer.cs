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
    public List<WorkerTile> WorkerTiles { get; private set; } = new List<WorkerTile>();
    private LocationType _locationType;

    [SerializeField] private Image[] _playerIconSlot;
    [SerializeField] private TextMeshProUGUI _locationName;
    Dictionary<Player, Image> _usedPlayerIcons = new Dictionary<Player, Image>();

    private void Awake()
    {
        if (_romeContainer == null)
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
            OnRemoveWorkerButtonClick();
        });
        _addWorkerButton.onClick.AddListener(() =>
        {
            OnAddWorkerButtonClick();
        });
    }

    private void Start()
    {
        GameFlowManager.Instance.HireWorkerEvent += OnHireWorkerEvent;
        GameFlowManager.Instance.BribeWorkerEvent += OnBribeWorkerEvent;
    }

    public Transform GetWorkersContainer()
    {
        return _workersContainer;
    }

    // Transfer a worker from the neutral labour pool to the building site
    private void OnAddWorkerButtonClick()
    {
        WorkerTile lastNeutralWorkerTile = GetLastNeutralTile();

        if (lastNeutralWorkerTile == null) return;

        MoveWorkerToNewLocation(lastNeutralWorkerTile, _locationType, 3);
    }

    // Transfer a worker from the building site to the neutral labour pool
    private void OnRemoveWorkerButtonClick()
    {
        if (WorkerTiles.Count <= 0) return;

        WorkerTile lastWorkerTile = WorkerTiles[WorkerTiles.Count - 1];

        MoveWorkerToNewLocation(lastWorkerTile, LocationType.Rome);
    }

    public void OnHireWorkerEvent(object sender, HireWorkerEvent e)
    {
        Player player = PlayerManager.Instance.Players[e.Employer];

        if (player.Monument.ConstructionSite != _locationType) return;

        if (e.Worker.Location.LocationType != LocationType.Rome)
        {
            return;
        }

        if (e.Employer == PlayerNumber.None)
        {
            return;
        }

        WorkerTile lastNeutralWorkerTile = GetLastNeutralTile();

        if (lastNeutralWorkerTile == null) return;

        MoveWorkerToNewLocation(lastNeutralWorkerTile, player.Monument.ConstructionSite, e.ContractLength);
    }

    public void OnBribeWorkerEvent(object sender, BribeWorkerEvent e)
    {
        if (e.Employer == PlayerNumber.None)
        {
            return;
        }

        ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(e.Worker.Location.LocationType);
        if (labourPoolLocation.LocationType != LocationType.Rome) return;

        CityWorkerTile oldWorkerTile = e.Worker.UIWorkerTile as CityWorkerTile;

        Player newEmployer = PlayerManager.Instance.Players[e.Employer];
        LocationType newLocation = newEmployer.Monument.ConstructionSite;

        if (newLocation != _locationType)
        {
            return;
        }

        int contractLength = e.Worker.ServiceLength;

        MoveWorkerToNewLocation(oldWorkerTile, newLocation, contractLength);
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

    // Remove worker tile from old location, add worker tile to new location. Make sure labour pool references are not broken
    public static void MoveWorkerToNewLocation(WorkerTile oldWorkerTile, LocationType newLocationType, int serviceLength = -1)
    {
        IWorker transferredWorker = oldWorkerTile.Worker;

        MonumentLocationUIContainer oldLocationMonumentUIContainer = NavigationManager.Instance.GetConstructionSiteContainers(oldWorkerTile.Worker.Location.LocationType);
        MonumentLocationUIContainer newLocationMonumentUIContainer = NavigationManager.Instance.GetConstructionSiteContainers(newLocationType);

        UILocationContainer romeContainer = NavigationManager.Instance.GetLocationUIContainer(LocationType.Rome) as UILocationContainer;

        if (oldLocationMonumentUIContainer == null)
        {
            romeContainer.WorkerTiles.Remove(oldWorkerTile);
        }
        else
        {
            oldLocationMonumentUIContainer.WorkerTiles.Remove(oldWorkerTile);
        }

        LabourPoolHandler.RemoveCityWorkerFromLabourPool(Rome.LabourPoolWorkers, transferredWorker);// ???
        oldWorkerTile.Destroy();

        WorkerTile newWorkerTile = AddWorkerTile(newLocationMonumentUIContainer);

        IWorkerLocation buildingSiteLocation = LocationManager.Instance.GetWorkerLocation(newLocationType);

        LabourPoolHandler.AddCityWorkerToLabourPool(Rome.LabourPoolWorkers, buildingSiteLocation);

        //newWorkerTile.Initialise(newLocationType, transferredWorker);
        newWorkerTile.Initialise(newLocationType, Rome.LabourPoolWorkers[Rome.LabourPoolWorkers.Count - 1]);
        newWorkerTile.Worker = Rome.LabourPoolWorkers[Rome.LabourPoolWorkers.Count - 1];

        if (serviceLength != -1 && newWorkerTile.Worker.Employer != PlayerNumber.None)
        {
            newWorkerTile.UpdateServiceLength(serviceLength);
        }
    }

    private static WorkerTile AddWorkerTile(MonumentLocationUIContainer newLocationMonumentUIContainer)
    {
        if(newLocationMonumentUIContainer == null)
        {
            UILocationContainer romeContainer = NavigationManager.Instance.GetLocationUIContainer(LocationType.Rome) as UILocationContainer;
            return AddWorkerTileToRome(romeContainer);
        }
        else
        {
            return AddWorkerTileToContructionSite(newLocationMonumentUIContainer);
        }
    }

    private static WorkerTile AddWorkerTileToContructionSite(MonumentLocationUIContainer newLocationMonumentUIContainer)
    {
        LocationType newWorkerLocationType = newLocationMonumentUIContainer._locationType;
        IWorkerLocation buildingSiteLocation = LocationManager.Instance.GetWorkerLocation(newWorkerLocationType);

        // Add worker tile to construction site
        GameObject workerPrefab = buildingSiteLocation.GetWorkerPrefabForLocation();
        GameObject workerGO = GameObject.Instantiate(workerPrefab, newLocationMonumentUIContainer.GetWorkersContainer());
        WorkerTile workerTile = workerGO.GetComponent<WorkerTile>();

        if (workerTile == null)
        {
            Debug.LogError($"Could not parse worker as a CityWorkerTile");
            return null;
        }

        newLocationMonumentUIContainer.WorkerTiles.Add(workerTile);

        return workerTile;
    }

    private static WorkerTile AddWorkerTileToRome(UILocationContainer romeContainer)
    {
        WorkerTile workerTile = romeContainer.AddWorkerTile();

        return workerTile;
    }

    private WorkerTile GetLastNeutralTile()
    {
        List<WorkerTile> romeWorkerTiles = _romeContainer.WorkerTiles;

        if (romeWorkerTiles.Count < 1) return null; // There should be neutral workers available in the shared worker pool

        WorkerTile lastNeutralWorkerTile = _romeContainer.WorkerTiles[_romeContainer.WorkerTiles.Count - 1];

        return lastNeutralWorkerTile;
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
