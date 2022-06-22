
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonumentComponentListItem : MonoBehaviour
{
    private MonumentComponentBlueprint _monumentComponentBlueprint;
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonBackground;
    [SerializeField] private GameObject _stateIconGO;
    [SerializeField] private Image _stateIconImage;

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
        if (_stateIconImage == null)
        {
            Debug.LogError($"could not find _stateIconImage on {gameObject.name}");
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

    // force-update the completion state of a monument component
    private void OnClickItem()
    {
        if (_buttonState is MonumentComponentDisplayButtonLockedState)
        {
            return;
        }

        MonumentComponentState monumentComponentState = _playersTabContainer.HandleMonumentComponentState(_monumentComponentBlueprint);

        switch (monumentComponentState)
        {
            case MonumentComponentState.Locked:
                break;
            case MonumentComponentState.Unaffordable:
                _buttonState = new MonumentComponentDisplayButtonUnaffordableState();
                break;
            case MonumentComponentState.Buildable:
                _buttonState = new MonumentComponentDisplayButtonAvailableState();
                break;
            case MonumentComponentState.InProgress:
                _buttonState = new MonumentComponentDisplayButtonInProgressState();
                break;
            case MonumentComponentState.Complete:
                _buttonState = new MonumentComponentDisplayButtonCompletedState();
                break;
            default:
                Debug.LogError($"Unknown state {monumentComponentState}");
                break;
        }

        Player player = PlayerManager.Instance.Players[_playersTabContainer.CurrentPlayerTab.PlayerNumber];
        MonumentComponent monumentComponent = player.Monument.GetMonumentComponentByType(_monumentComponentBlueprint.MonumentComponentType);
        UpdateSublabelForMonumentComponentState(monumentComponentState, monumentComponent);

        _buttonState.UpdateUIForButtonState(this, _buttonBackground);
    }


    public void UpdateUIForButtonState(Monument monument)
    {
        MonumentComponent monumentComponent = monument.GetMonumentComponentByType(_monumentComponentBlueprint.MonumentComponentType);
        MonumentComponentState monumentComponentState = monumentComponent.State;

        switch (monumentComponentState)
        {
            case MonumentComponentState.Locked:
                _buttonState = new MonumentComponentDisplayButtonLockedState();
                break;
            case MonumentComponentState.Unaffordable:
                _buttonState = new MonumentComponentDisplayButtonUnaffordableState();
                break;
            case MonumentComponentState.Buildable:
                _buttonState = new MonumentComponentDisplayButtonAvailableState();
                break;
            case MonumentComponentState.InProgress:
                _buttonState = new MonumentComponentDisplayButtonInProgressState();
                break;
            case MonumentComponentState.Complete:
                _buttonState = new MonumentComponentDisplayButtonCompletedState();
                break;
            default:
                Debug.LogError($"Unknown state {monumentComponentState}");
                break;
        }

        UpdateSublabelForMonumentComponentState(monumentComponentState, monumentComponent);

        _buttonState.UpdateUIForButtonState(this, _buttonBackground);
    }

    private void UpdateSublabelForMonumentComponentState(MonumentComponentState monumentComponentState, MonumentComponent monumentComponent)
    {
        Player player = PlayerManager.Instance.Players[_playersTabContainer.CurrentPlayerTab.PlayerNumber];
        List<IResource> resourceCosts = monumentComponent.MonumentComponentBlueprint.ResourceCosts;

        if (monumentComponentState == MonumentComponentState.Unaffordable ||
            monumentComponentState == MonumentComponentState.Buildable)
        {
            string subLabelText = "";

            _componentSubLabel.enabled = true;

            for (int i = 0; i < resourceCosts.Count; i++)
            {
                IPlayerStat playerStat = resourceCosts[i] as IPlayerStat;

                ResourceType resourceType = resourceCosts[i].GetResourceType();
                bool canAffordResource = player.Resources[resourceType].Amount >= resourceCosts[i].Amount;

                if (canAffordResource)
                {
                    subLabelText += $"{AssetManager.Instance.GetPlayerStatInlineIcon(playerStat)} {resourceCosts[i].Amount}    ";
                }
                else
                {
                    subLabelText += $"{AssetManager.Instance.GetPlayerStatInlineIcon(playerStat)} <color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{resourceCosts[i].Amount}</color>    ";
                }
            }

            _componentSubLabel.text = subLabelText;
        }
        else
        {
            _componentSubLabel.enabled = false;
        }
    }

    public GameObject GetLockGameObject()
    {
        return _stateIconGO;
    }

    public void SetIcon(Sprite icon)
    {
        _stateIconImage.sprite = icon;
    }
}

