using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolActionPlayerSelectionTileElement : MonoBehaviour, IUIToolGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    private PlayerNumber _playerNumber;

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

    public void Initialise(IUIToolGameActionStep uiToolGameActionStep)
    {
        List<IUIToolGameActionElement> existingElements = uiToolGameActionStep.GetUIElements();

        int existingPlayerTiles = 0;
        for (int i = 0; i < existingElements.Count; i++)
        {
            if(existingElements[i] is UIToolActionPlayerSelectionTileElement)
            {
                existingPlayerTiles++;
            }
        }

        if(existingPlayerTiles == 0)
        {
            _playerNumber = PlayerNumber.Player1;
        }
        else if(existingPlayerTiles == 1)
        {
            _playerNumber = PlayerNumber.Player2;
        }
        else
        {
            _playerNumber = PlayerNumber.Player3;
        }

        Player player = PlayerManager.Instance.Players[_playerNumber];
        _buttonLabel.text = $"{player.Name}";

    }

    private void OnClick()
    {
        Debug.Log($"Select player {_playerNumber}");
    }
}
