using UnityEngine;
using UnityEngine.UI;

public class ResourcesWorkerTile : WorkerTile
{
    [SerializeField] private Button _employerButton;

    private void Awake()
    {
        if (_employerButton == null)
        {
            Debug.LogError($"Could not find _employerButton");
        }
        if (_tileBackground == null)
        {
            Debug.LogError($"Could not find _tileBackground");
        }
        if (_workerIcon == null)
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

        _employerButton.onClick.AddListener(() =>
        {
            SetNextEmployer();
        });

        _serviceLengthInputField.onValueChanged.AddListener(delegate { OnChangeServiceLengthInputField(); });

    }

    public override void Initialise(LocationType locationType, IWorker worker)
    {
        _locationType = locationType;
        Worker = worker;

        Worker.SetUIWorkerTile(this);

        SetEmployer(PlayerNumber.None);
    }

    public override void SetEmployer(PlayerNumber newEmployer)
    {
        if (newEmployer == PlayerNumber.None)
        {
            //_statusText.gameObject.SetActive(false);
            _serviceLengthInputField.gameObject.SetActive(false);
        }
        else
        {
            //_statusText.gameObject.SetActive(true);
            _serviceLengthInputField.gameObject.SetActive(true);
        }
        Worker.SetEmployer(newEmployer);
    }

    public void SetNextEmployer()
    {
        switch (Worker.Employer)
        {
            case PlayerNumber.Player1:
                SetEmployer(PlayerNumber.Player2);
                break;
            case PlayerNumber.Player2:
                SetEmployer(PlayerNumber.Player3);
                break;
            case PlayerNumber.Player3:
                SetEmployer(PlayerNumber.None);
                break;
            case PlayerNumber.None:
                SetEmployer(PlayerNumber.Player1);
                UpdateServiceLength(3);
                break;
            default:
                break;
        }

        SetButtonColour(Worker.Employer);
    }

    protected override void SetWorkerToNeutral()
    {
        SetEmployer(PlayerNumber.None);
        SetButtonColour(PlayerNumber.None);
    }

    private void SetButtonColour(PlayerNumber playerNumber)
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
}
