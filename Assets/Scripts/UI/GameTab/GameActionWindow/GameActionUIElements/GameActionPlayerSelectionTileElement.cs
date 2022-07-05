using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionPlayerSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    public PlayerNumber PlayerNumber { get; private set; }
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

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _uiToolGameActionStep = uiToolGameActionStep as IUIPlayerSelectionGameActionStep;
        if(_uiToolGameActionStep == null)
        {

            Debug.LogError($"Could not parse {uiToolGameActionStep.GetType()} as a IUIPlayerSelectionGameActionStep");
        }
        List<IGameActionElement> existingElements = uiToolGameActionStep.GetUIElements();

        int existingPlayerTiles = 0;
        for (int i = 0; i < existingElements.Count; i++)
        {
            if(existingElements[i] is GameActionPlayerSelectionTileElement)
            {
                existingPlayerTiles++;
            }
        }

        if(existingPlayerTiles == 0)
        {
            PlayerNumber = PlayerNumber.Player1;
        }
        else if(existingPlayerTiles == 1)
        {
            PlayerNumber = PlayerNumber.Player2;
        }
        else
        {
            PlayerNumber = PlayerNumber.Player3;
        }

        Player player = PlayerManager.Instance.Players[PlayerNumber];
        _buttonLabel.text = $"{player.Name}";

    }

    private void OnClick()
    {
        _uiToolGameActionStep.SelectPlayer(PlayerNumber);
    }

    public void Select()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.SelectedBackground);
    }

    public void Deselect()
    {
        _button.image.color = ColourUtility.GetColour(ColourType.Empty);
    }
}
