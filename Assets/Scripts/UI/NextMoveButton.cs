using TMPro;
using UnityEngine;

public class NextMoveButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;

    private void Awake()
    {
        if(_textField == null)
        {
            Debug.LogError($"Cannot find text field on Next Move Button");
        }
    }

    public void ExecuteNextRound()
    {
        if (GameActionStepHandler.CurrentGameActionSequence != null) return;

        GameFlowManager.Instance.ExecuteNextGameStep();
    }

    public void UpdateText()
    {
        TimeOfDay timeOfDay = GameFlowManager.Instance.TimeOfDay;

        switch (timeOfDay)
        {
            case TimeOfDay.Morning:
                _textField.text = "To Noon";
                break;
            case TimeOfDay.Noon:
                _textField.text = "To Afternoon";
                break;
            case TimeOfDay.Afternoon:
                _textField.text = "Next Day";
                break;
            default:
                break;
        }
    }
}
