
using UnityEngine;

public class UIToolGameActionAssetHandler : MonoBehaviour
{
    public static UIToolGameActionAssetHandler Instance;

    [SerializeField] private GameObject _uiToolGameStepLabelPrefab;
    [SerializeField] private GameObject _uiToolGameActionButtonPrefab;
    [SerializeField] private GameObject _uiToolActionWindowPrefab;

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

        Instance = this;
    }

    public GameObject GetStepLabelPrefab()
    {
        return _uiToolGameStepLabelPrefab;
    }
    public GameObject GetNextActionStepButton()
    {
        return _uiToolGameActionButtonPrefab;
    }

    public GameObject GetUIToolActionWindowPrefab()
    {
        return _uiToolActionWindowPrefab;
    }
}
