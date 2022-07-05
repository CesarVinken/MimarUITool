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
        if(GameActionHandler.CurrentGameActionSequence != null &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            GameActionHandler.CurrentGameActionSequence.CloseGameActionWindow();
        }
    }

    private void BeginActionSequenceProcudure()
    {
        if (GameActionHandler.CurrentGameActionSequence != null) return;

        GameActionHandler toolGameActionHandler = new GameActionHandler();
    }
}
