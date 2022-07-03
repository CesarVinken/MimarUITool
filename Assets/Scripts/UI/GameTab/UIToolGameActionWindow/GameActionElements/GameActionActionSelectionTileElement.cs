
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionActionSelectionTileElement : MonoBehaviour, IUIToolGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    public UIToolGameActionType GameActionType { get; private set; }
    private ActionPickStep _actionPickStep;

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
        _buttonLabel.text = $"{GameActionType}";

    }

    public void SetUp(UIToolGameActionType gameActionType, ActionPickStep actionPickStep)
    {
        GameActionType = gameActionType;
        _actionPickStep = actionPickStep;
    }

    private void OnClick()
    {
        _actionPickStep.SelectAction(GameActionType);
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
