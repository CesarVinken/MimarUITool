using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class CityWorkerTile : WorkerTile
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private List<MonumentComponent> _dropdownOptions = new List<MonumentComponent>();

    private void Awake()
    {
        if (_tileBackground == null)
        {
            Debug.LogError($"Could not find _tileBackground");
        }
        if (_workerIcon == null)
        {
            Debug.LogError($"Could not find _workerIcon");
        }
        if (_dropdown == null)
        {
            Debug.LogError($"Could not find _workerIcon");
        }
        //if (_stateText == null)
        //{
        //    Debug.LogError($"Could not find _stateText");
        //}
        if (_serviceLengthInputField == null)
        {
            Debug.LogError($"Could not find _serviceLengthInputField");
        }

        _serviceLengthInputField.onValueChanged.AddListener(delegate { OnChangeServiceLengthInputField(); });
    }

    private void Start()
    {
        GameFlowManager.Instance.MonumentComponentCompletionStateChangeEvent += OnMonumentComponentCompletionChange;
    }

    public override void Initialise(LocationType locationType, IWorker worker)
    {
        _locationType = locationType;

        Worker = worker;
        Worker.SetUIWorkerTile(this);

        switch (_locationType)
        {
            case LocationType.ConstructionSite1:
                SetEmployer(PlayerNumber.Player1);
                break;
            case LocationType.ConstructionSite2:
                SetEmployer(PlayerNumber.Player2);
                break;
            case LocationType.ConstructionSite3:
                SetEmployer(PlayerNumber.Player3);
                break;
            default:
                SetEmployer(PlayerNumber.None);
                break;
        }

        SetIconColour(Worker.Employer);
        if(Worker.Employer == PlayerNumber.None)
        {
            return;
        }

        UpdateServiceLength(3);
        InitialiseTaskDropdown(); // could move to its own class
    }

    public override void SetEmployer(PlayerNumber newEmployer)
    {
        if (newEmployer == PlayerNumber.None)
        {
            //_stateText.gameObject.SetActive(false);
            _dropdown.gameObject.SetActive(false);
            _serviceLengthInputField.gameObject.SetActive(false);
        }
        else
        {
            //_stateText.gameObject.SetActive(true);
            _dropdown.gameObject.SetActive(true);
            _serviceLengthInputField.gameObject.SetActive(true);
        }
        Worker.SetEmployer(newEmployer);
    }

    private void InitialiseTaskDropdown()
    {
        _dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(_dropdown);
        });

        UpdateDropdownComponentList();
    }

    // The available items in the dropdown changes whenver a player completes a part of the monument 
    public void UpdateDropdownComponentList()
    {
        CityWorker cityWorker = Worker as CityWorker;
        MonumentComponent previouslySelectedComponent = null;

        if (_dropdown.value > 0)
        {
            previouslySelectedComponent = _dropdownOptions[_dropdown.value - 1];
        }

        _dropdown.ClearOptions();
        _dropdownOptions.Clear();

        Player player = PlayerManager.Instance.Players[Worker.Employer];

        List<MonumentComponent> unfinishedMonumentComponents = new List<MonumentComponent>();
        List<MonumentComponent> allMonumentComponents = player.Monument.GetMonumentComponents();

        for (int i = 0; i < allMonumentComponents.Count; i++)
        {
            MonumentComponent component = allMonumentComponents[i];
            if (component.State != MonumentComponentState.InProgress) continue;

            unfinishedMonumentComponents.Add(component);
        }

        List<OptionData> options = new List<OptionData>();

        options.Add(new OptionData("Unassigned"));

        for (int i = 0; i < unfinishedMonumentComponents.Count; i++)
        {
            options.Add(new OptionData(unfinishedMonumentComponents[i].Name));
            _dropdownOptions.Add(unfinishedMonumentComponents[i]);
        }
        _dropdown.AddOptions(options);

        // After updating the component list, select the currently worked on Component is there is one
        if (cityWorker.CurrentBuildingTask != null && previouslySelectedComponent != null)
        {
            MonumentComponent overlapComponent = _dropdownOptions.SingleOrDefault(o => o.Equals(previouslySelectedComponent));

            if (overlapComponent != null)
            {
                _dropdown.value = _dropdownOptions.IndexOf(overlapComponent) + 1;
            }
        }
    }

    private void DropdownValueChanged(TMP_Dropdown change)
    {
        int dropdownIndex = change.value;

        CityWorker worker = Worker as CityWorker;

        if(worker == null)
        {
            Debug.LogError($"Cannot find city worker");
            return;
        }

        if (dropdownIndex == 0) // dropdownIndex 0 == UNASSIGNED
        {
            worker.SetCurrentBuildingTask(null);
            return;
        }

        worker.SetCurrentBuildingTask(_dropdownOptions[dropdownIndex - 1]);
    }

    public void SetIconColour(PlayerNumber playerNumber)
    {
        if (PlayerManager.Instance.Players.TryGetValue(playerNumber, out Player player))
        {
            _workerIcon.color = player.PlayerColour;
        }
        else
        {
            _workerIcon.color = ColourUtility.GetColour(ColourType.Empty);
        }
    }

    protected override void SetWorkerToNeutral()
    {
        MonumentLocationUIContainer constructionSiteContainer = NavigationManager.Instance.GetLocationUIContainer(Worker.Location.LocationType) as MonumentLocationUIContainer;
        
        if(constructionSiteContainer == null)
        {
            Debug.LogError($"Could not parse constructionSiteContainer as a MonumentLocationUIContainer");
        }

        constructionSiteContainer.RemoveWorkerFromSite(this);
    }

    // A player completes a monument component
    public void OnMonumentComponentCompletionChange(object sender, MonumentComponentCompletionStateChangeEvent e)
    {
        if (e.AffectedPlayer != Worker.Employer) return;

        CityWorker worker = Worker as CityWorker;
        if (worker.CurrentBuildingTask?.MonumentComponentType == e.AffectedComponent.MonumentComponentType)
        {
            worker.SetCurrentBuildingTask(null); // Unassign worker from current job
        }

        UpdateDropdownComponentList();
    }

    public override void Destroy()
    {
        if (Worker.Employer != PlayerNumber.None)
        {
            PlayerManager.Instance.Players[Worker.Employer].RemoveWorker(Worker);
        }

        GameFlowManager.Instance.MonumentComponentCompletionStateChangeEvent -= OnMonumentComponentCompletionChange;
        Destroy(gameObject);
    }
}
