using UnityEngine;

public class MonumentDisplayComponent : MonoBehaviour
{
    [SerializeField] private Transform _monumentContainer;
    [SerializeField] private MonumentDisplay _monumentDisplay;

    public MonumentComponentType MonumentComponentType { get; private set; }

    public void Initialise()
    {
        if (_monumentContainer == null)
        {
            _monumentContainer = transform.parent;
        }

        _monumentDisplay = _monumentContainer.GetComponent<MonumentDisplay>();

        if (_monumentDisplay == null)
        {
            Debug.LogError($"Could not find a MonumentDisplay component on the parent of {gameObject.name}");
        }

        _monumentDisplay.AddToMonumentDisplayComponents(this);
    }

    public void SetMonumentComponentType(MonumentComponentType monumentComponentType)
    {
        MonumentComponentType = monumentComponentType;
    }

}
