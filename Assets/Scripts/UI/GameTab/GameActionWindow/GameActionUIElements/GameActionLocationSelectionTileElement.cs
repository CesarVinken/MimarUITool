using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionLocationSelectionTileElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;
    private PickTargetLocationStep _uiToolGameActionStep;

    public ILocation TargetLocation { get; private set; }
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

    public void SetUp(ILocation location, PickTargetLocationStep locationSelectionGameActionStep)
    {
        TargetLocation = location;
        _uiToolGameActionStep = locationSelectionGameActionStep;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _buttonLabel.text = $"{TargetLocation.Name}";
    }

    private void OnClick()
    {
        _uiToolGameActionStep.SelectLocation(TargetLocation.LocationType);
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
