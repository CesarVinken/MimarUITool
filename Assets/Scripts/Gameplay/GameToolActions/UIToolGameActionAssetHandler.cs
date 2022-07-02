
using UnityEngine;

public class UIToolGameActionAssetHandler : MonoBehaviour
{
    public static UIToolGameActionAssetHandler Instance;

    [SerializeField] private GameObject _uiToolGameStepLabelPrefab;
    [SerializeField] private GameObject _uiToolGameActionButtonPrefab;
    [SerializeField] private GameObject _uiToolActionWindowPrefab;
    [SerializeField] private GameObject _uiToolPlayerSelectionTilePrefab;

    private void Awake()
    {
        if (_uiToolGameStepLabelPrefab == null)
        {
            Debug.LogError($"Could not find _uiToolGameStepLabelPrefab");
        }
        if (_uiToolGameActionButtonPrefab == null)
        {
            Debug.LogError($"Could not find _uiToolGameActionButtonPrefab");
        }
        if (_uiToolActionWindowPrefab == null)
        {
            Debug.LogError($"Could not find _uiToolActionWindowPrefab");
        }
        if (_uiToolPlayerSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _uiToolPlayerSelectionTilePrefab");
        }

        Instance = this;
    }

    public GameObject GetStepLabelPrefab()
    {
        return _uiToolGameStepLabelPrefab;
    }
    public GameObject GetNextActionStepButtonPrefab()
    {
        return _uiToolGameActionButtonPrefab;
    }

    public GameObject GetPlayerSelectionTilePrefab()
    {
        return _uiToolPlayerSelectionTilePrefab;
    }

    public GameObject GetUIToolActionWindowPrefab()
    {
        return _uiToolActionWindowPrefab;
    }
}
