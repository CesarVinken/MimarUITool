
using UnityEngine;

public class UIToolGameActionAssetHandler : MonoBehaviour
{
    public static UIToolGameActionAssetHandler Instance;

    [SerializeField] private GameObject _uiToolGameStepLabelPrefab;
    [SerializeField] private GameObject _uiToolGameActionButtonPrefab;
    [SerializeField] private GameObject _uiToolActionWindowPrefab;
    [SerializeField] private GameObject _uiToolPlayerSelectionTilePrefab;
    [SerializeField] private GameObject _uiToolActionSelectionTilePrefab;

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
        if (_uiToolActionSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _uiToolActionSelectionTilePrefab");
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

    public GameObject GetActionSelectionTilePrefab()
    {
        return _uiToolActionSelectionTilePrefab;
    }
}
