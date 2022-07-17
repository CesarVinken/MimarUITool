using TMPro;
using UnityEngine;

public class GameActionMainContentTextBlockElement : MonoBehaviour, IGameActionElement
{
    [SerializeField] private TextMeshProUGUI _label;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetText(string content)
    {
        _label.text = content;
    }

    public void Initialise(IGameActionStep uiToolGameActionStep)
    {

    }
}
