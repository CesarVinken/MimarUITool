using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameActionNextStepButtonElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonLabel;

    private IGameActionStep _uiToolGameActionStep = null;

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
        _uiToolGameActionStep.NextStep();
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {
        _uiToolGameActionStep = uiToolGameActionStep;

        if(uiToolGameActionStep is CheckoutStep)
        {
            _buttonLabel.text = "Execute action";
        }
        else
        {
            _buttonLabel.text = "Next";
        }
    }
}
