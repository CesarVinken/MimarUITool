using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionWorkerSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;
    private PickWorkerGameActionStep _uiToolGameActionStep;
    [SerializeField] protected Image _tileBackground;
    [SerializeField] protected Image _workerIcon;

    public IWorker Worker { get; private set; }
    public WorkerActionType WorkerActionType { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void Awake()
    {
        if (_button == null)
        {
            Debug.LogError($"could not find button on {gameObject.name}");
        }
        if (_buttonLabel == null)
        {
            Debug.LogError($"could not find buttonLabel on {gameObject.name}");
        }
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    public void SetUp(IWorker worker, PickWorkerGameActionStep pickWorkerGameActionStep, WorkerActionType workerActionType)
    {
        Worker = worker;
        WorkerActionType = workerActionType;
        _uiToolGameActionStep = pickWorkerGameActionStep;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _buttonLabel.text = $"Worker";
        SetWorkerIcon();
    }

    private void SetWorkerIcon()
    {
        _workerIcon.sprite = AssetManager.Instance.GetWorkerIcon(Worker);

        if(Worker.Employer == PlayerNumber.None)
        {
            _workerIcon.color = ColourUtility.GetColour(ColourType.Empty);
        }
        else
        {
            Player player = PlayerManager.Instance.Players[Worker.Employer];
            _workerIcon.color = player.PlayerColour;
        }
    }

    private void OnClick()
    {
        _uiToolGameActionStep.SelectWorker(this);
    }

    public void Select()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
    }

    public void Deselect()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.Empty);
    }

    public void MakeUnavailable()
    {
        _button.interactable = false;
        _button.image.color = ColourUtility.GetColour(ColourType.GrayedOut);
        IsAvailable = false;
    }

    public void MakeAvailable()
    {
        _button.interactable = true;
        IsAvailable = true;
    }
}
