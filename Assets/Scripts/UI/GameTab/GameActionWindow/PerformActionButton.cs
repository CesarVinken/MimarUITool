using UnityEngine;
using UnityEngine.UI;

public class PerformActionButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        if(_button == null)
        {
            Debug.LogError($"Could not find PerformActionButton");
        }

        _button.onClick.AddListener(delegate { BeginActionSequenceProcudure(); });
    }

    public void Update()
    {
        if(GameActionStepHandler.CurrentGameActionSequence != null &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            GameActionStepHandler.CurrentGameActionSequence.CloseGameActionWindow();
        }
    }

    private void BeginActionSequenceProcudure()
    {
        if (GameActionStepHandler.CurrentGameActionSequence != null) return;

        GameActionStepHandler toolGameActionHandler = new GameActionStepHandler();
    }
}
