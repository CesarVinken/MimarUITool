
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonumentComponentListItem : MonoBehaviour
{
    private MonumentComponentBlueprint _monumentComponentBlueprint;
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonBackground;
    [SerializeField] private GameObject _stateIconGO;

    [SerializeField] private TextMeshProUGUI _componentNameLabel;
    [SerializeField] private TextMeshProUGUI _componentSubLabel;
    private PlayersTabContainer _playersTabContainer;
    private MonumentComponentDisplayButtonState _buttonState;

    private void Awake()
    {
        if (_button == null)
        {
            Debug.LogError($"could not find _button on {gameObject.name}");
        }
        if (_buttonBackground == null)
        {
            Debug.LogError($"could not find _buttonBackground on {gameObject.name}");
        }
        if (_stateIconGO == null)
        {
            Debug.LogError($"could not find _stateIconGO on {gameObject.name}");
        }
        if (_componentNameLabel == null)
        {
            Debug.LogError($"could not find _componentNameLabel on {gameObject.name}");
        }
        if (_componentSubLabel == null)
        {
            Debug.LogError($"could not find _componentSubLabel on {gameObject.name}");
        }
    }

    private void Start()
    {
        _button.onClick.AddListener(delegate { OnClickItem(); });
    }
    public void Initialise(MonumentComponentBlueprint blueprint, PlayersTabContainer playersTabContainer)
    {
        _monumentComponentBlueprint = blueprint;
        _playersTabContainer = playersTabContainer;
        SetName(blueprint.Name);
    }

    private void SetName(string itemName)
    {
        _componentNameLabel.text = itemName;
        gameObject.name = itemName;
    }

    // force-update the completion status of a monument component
    private void OnClickItem()
    {
        if(_buttonState is MonumentComponentDisplayButtonLockedState)
        {
            return;
        }

        bool isCompleted = _playersTabContainer.HandleMonumentComponentCompletion(_monumentComponentBlueprint);

        if (isCompleted)
        {
            _buttonState = new MonumentComponentDisplayButtonCompletedState();
        }
        else
        {
            _buttonState = new MonumentComponentDisplayButtonAvailableState();
        }

        _buttonState.UpdateUIForButtonState(this, _buttonBackground);
    }

    public void UpdateUIForButtonState(Monument monument)
    {
        MonumentComponent monumentComponent = monument.GetMonumentComponentByType(_monumentComponentBlueprint.MonumentComponentType);
        bool componentIsComplete = monumentComponent.IsComplete;

        if (componentIsComplete)
        {
            _buttonState = new MonumentComponentDisplayButtonCompletedState();
        }
        else
        {
            _buttonState = new MonumentComponentDisplayButtonAvailableState();
        }

        _buttonState.UpdateUIForButtonState(this, _buttonBackground);
    }

    public GameObject GetLockGameObject()
    {
        return _stateIconGO;
    }
}

