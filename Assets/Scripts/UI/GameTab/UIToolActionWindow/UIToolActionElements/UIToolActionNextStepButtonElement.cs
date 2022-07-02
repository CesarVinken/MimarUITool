using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToolActionNextStepButtonElement : MonoBehaviour, IUIToolGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    private UIToolGameActionHandler _gameActionHandler = null;

    public Transform GetTransform()
    {
        return transform;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void Awake()
    {
        if(_button == null)
        {
            Debug.LogError($"could not find button on {gameObject.name}");
        }
        if(_buttonLabel == null)
        {
            Debug.LogError($"could not find buttonLabel on {gameObject.name}");
        }
        _button.onClick.AddListener(delegate { OnClick(); });
    }

    private void OnClick()
    {
        UIToolGameActionHandler.CurrentUIGameToolAction.NextStep();
    }

    public void Initialise(IUIToolGameActionStep uiToolGameActionStep)
    {
        int stepsInSequence = UIToolGameActionHandler.CurrentUIGameToolAction.GetSteps().Count;
        if(stepsInSequence == uiToolGameActionStep.StepNumber)
        {
            _buttonLabel.text = "Execute action";
        }
        else
        {
            _buttonLabel.text = "Next";
        }
    }
}