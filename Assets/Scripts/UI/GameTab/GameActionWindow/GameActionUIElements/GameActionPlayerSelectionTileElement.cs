using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionPlayerSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    public Player Player { get; private set; }
    private IUIPlayerSelectionGameActionStep _uiToolGameActionStep;

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

    public void SetUp(Player player)
    {
        Player = player;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _uiToolGameActionStep = uiToolGameActionStep as IUIPlayerSelectionGameActionStep;

        if(_uiToolGameActionStep == null)
        {
            Debug.LogError($"Could not parse {uiToolGameActionStep.GetType()} as a IUIPlayerSelectionGameActionStep");
        }

        List<IGameActionElement> existingElements = uiToolGameActionStep.GetUIElements();
        _buttonLabel.text = $"{Player.Name}";
    }

    private void OnClick()
    {
        _uiToolGameActionStep.SelectPlayer(Player.PlayerNumber);
    }

    public void Select()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
    }

    public void Deselect()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.Empty);
    }

    public void Deactivate()
    {
        _button.interactable = false;
        _button.image.color = ColourUtility.GetColour(ColourType.GrayedOut);
    }

    public void Activate()
    {
        _button.interactable = true;
    }
}
