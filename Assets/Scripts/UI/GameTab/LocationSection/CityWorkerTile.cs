using TMPro;
using UnityEngine;

public class CityWorkerTile : WorkerTile
{
    [SerializeField] private TMP_Dropdown _dropdown;
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
}
