using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class CityWorkerTile : WorkerTile
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private MonumentComponent _currentBuildingTask;

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
        //if (_statusText == null)
        //{
        //    Debug.LogError($"Could not find _statusText");
        //}
        if (_serviceLengthInputField == null)
        {
            Debug.LogError($"Could not find _serviceLengthInputField");
        }

        _serviceLengthInputField.onValueChanged.AddListener(delegate { OnChangeServiceLengthInputField(); });
    }

    private void Start()
    {
        GameFlowManager.Instance.MonumentComponentCompletionEvent += OnMonumentComponentCompletionChange;
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
        Debug.Log($"Worker.Employer {Worker.Employer}");

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
            //_statusText.gameObject.SetActive(false);
            _dropdown.gameObject.SetActive(false);
            _serviceLengthInputField.gameObject.SetActive(false);
        }
        else
        {
            //_statusText.gameObject.SetActive(true);
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
        _dropdown.ClearOptions();

        Player player = PlayerManager.Instance.Players[Worker.Employer];

        List<MonumentComponent> unfinishedMonumentComponents = player.Monument.GetMonumentComponents().Where(c => c.Complete == false).ToList();
        List<OptionData> options = new List<OptionData>();

        options.Add(new OptionData("Unassigned"));

        for (int i = 0; i < unfinishedMonumentComponents.Count; i++)
        {
            options.Add(new OptionData(unfinishedMonumentComponents[i].Name));
        }
        _dropdown.AddOptions(options);
    }

    private void DropdownValueChanged(TMP_Dropdown change)
    {
        //if (EditorManager.SelectedTileMainModifierCategoryIndex == change.value) return;

        //EditorTileMainModifierCategory mainModifierCategory = EditorTileMainModifierCategories[change.value];
        //Logger.Warning("New Dropdown Value : " + mainModifierCategory.Name);
        Debug.LogWarning("New Dropdown Value : ");

        //EditorManager.SelectedTileMainModifierCategoryIndex = change.value;

        //EditorCanvasUI.Instance.SelectedTileModifierContainer.SetCurrentlyAvailableModifierCategories(mainModifierCategory);
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
        MonumentLocationUIContainer constructionSiteContainer = NavigationManager.Instance.GetMonumentLocationUIContainer(Worker.Location.LocationType);

        constructionSiteContainer.RemoveWorkerFromSite(this);
    }


    public void OnMonumentComponentCompletionChange(object sender, MonumentComponentCompletionEvent e)
    {
        if (e.AffectedPlayer != Worker.Employer) return;

        if (_currentBuildingTask?.MonumentComponentType == e.AffectedComponent.MonumentComponentType)
        {
            Debug.Log($"unassign properly from current job");
            // TODO Unassign worker from current job
        }

        UpdateDropdownComponentList();
    }

    public override void Destroy()
    {
        if (Worker.Employer != PlayerNumber.None)
        {
            PlayerManager.Instance.Players[Worker.Employer].RemoveWorker(Worker);
        }

        GameFlowManager.Instance.MonumentComponentCompletionEvent -= OnMonumentComponentCompletionChange;
        Destroy(gameObject);
    }
}
