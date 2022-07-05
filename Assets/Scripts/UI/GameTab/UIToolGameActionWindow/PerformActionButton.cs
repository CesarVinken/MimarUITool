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

        _button.onClick.AddListener(delegate { BeginActionProcudure(); });
    }

    public void Update()
    {
        if(UIToolGameActionHandler.CurrentUIGameToolAction != null &&
            Input.GetKeyDown(KeyCode.Escape))
        {
            UIToolGameActionHandler.CurrentUIGameToolAction.CloseGameActionWindow();
        }
    }

    private void BeginActionProcudure()
    {
        if (UIToolGameActionHandler.CurrentUIGameToolAction != null) return;

        UIToolGameActionHandler toolGameActionHandler = new UIToolGameActionHandler();
    }
}
