using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionActionSelectionTileElement : MonoBehaviour, IUIToolGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    private UIToolGameActionType _gameActionType;

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
        _buttonLabel.text = $"{_gameActionType}";

    }

    public void SetUp(UIToolGameActionType gameActionType)
    {
        _gameActionType = gameActionType;
    }

    private void OnClick()
    {
        Debug.Log($"Select action {_gameActionType}");
    }
}
