using TMPro;
using UnityEngine;

public class GameActionInputFieldElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private TextMeshProUGUI _inputFieldLabel;
    [SerializeField] private TMP_InputField _inputField;

    public string InputFieldContent { get; private set; } = "";

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetLabel(string labelText)
    {
        _inputFieldLabel.text = labelText;
    }

    public void SetInputValue(int value)
    {
        _inputField.text = value.ToString();
    }

    public void Initialise(IGameActionStep gameActionStep)
    {
        if (_inputField == null)
        {
            Debug.LogError($"could not find _inputField on {gameObject.name}");
        }
        if (_inputFieldLabel == null)
        {
            Debug.LogError($"could not find _inputFieldLabel on {gameObject.name}");
        }

        if(gameActionStep is SetHiringTermStep) // we can only set a number as a contract length
        {
            SetHiringTermStep setHiringTermStep = gameActionStep as SetHiringTermStep;
            _inputField.characterValidation = TMP_InputField.CharacterValidation.Digit;
            _inputField.onValueChanged.AddListener(delegate { setHiringTermStep.OnInputFieldValueChanged(_inputField.text); });
        }

        _inputField.onValueChanged.AddListener(delegate { OnInputFieldValueChanged(); });
    }

    private void OnInputFieldValueChanged()
    {
        InputFieldContent = _inputField.text;
    }
}
