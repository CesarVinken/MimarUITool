using UnityEngine;

public class MonumentDisplayComponent : MonoBehaviour
{
    [SerializeField] private MeshRenderer _monumentComponentMeshRenderer;
    [SerializeField] private Transform _monumentContainer;
    [SerializeField] private MonumentDisplay _monumentDisplay;
    [SerializeField] private Material _texturedMaterial;

    public MonumentComponentType MonumentComponentType { get; private set; }

    public void Initialise()
    {
        if(_texturedMaterial == null)
        {
            Debug.LogError($"Could not find the textured material for the Monument Display Component {gameObject.name}");
        }

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

    public void SetMaterial(MonumentComponentVisibility visibility)
    {
        switch (visibility)
        {
            case MonumentComponentVisibility.Hidden:
                _monumentComponentMeshRenderer.material = AssetManager.Instance.GetEmptyMaterial();
                break;
            case MonumentComponentVisibility.InProgress:
                _monumentComponentMeshRenderer.material = AssetManager.Instance.GetEmptyMaterial();
                break;
            case MonumentComponentVisibility.Complete:
                _monumentComponentMeshRenderer.material = _texturedMaterial;
                break;
            default:
                new NotImplementedException("VisibilityState", visibility.ToString());
                break;
        }
    }
}
