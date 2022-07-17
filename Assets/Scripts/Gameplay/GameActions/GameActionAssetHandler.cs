using UnityEngine;

public class GameActionAssetHandler : MonoBehaviour
{
    public static GameActionAssetHandler Instance;

    [SerializeField] private GameObject _gameStepLabelPrefab;
    [SerializeField] private GameObject _nextStepButtonPrefab;
    [SerializeField] private GameObject _gameActionWindowPrefab;
    [SerializeField] private GameObject _playerSelectionTilePrefab;
    [SerializeField] private GameObject _actionSelectionTilePrefab;
    [SerializeField] private GameObject _locationSelectionTilePrefab;
    [SerializeField] private GameObject _constructionSiteUpgradeSelectionTilePrefab;
    [SerializeField] private GameObject _mainContentLabelPrefab;
    [SerializeField] private GameObject _workerSelectionTilePrefab;
    [SerializeField] private GameObject _inputFieldPrefab;


    private void Awake()
    {
        if (_gameStepLabelPrefab == null)
        {
            Debug.LogError($"Could not find _gameStepLabelPrefab");
        }
        if (_nextStepButtonPrefab == null)
        {
            Debug.LogError($"Could not find _gameActionButtonPrefab");
        }
        if (_gameActionWindowPrefab == null)
        {
            Debug.LogError($"Could not find _gameActionWindowPrefab");
        }
        if (_playerSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _playerSelectionTilePrefab");
        }
        if (_actionSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _actionSelectionTilePrefab");
        }
        if (_locationSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _locationSelectionTilePrefab ");
        }
        if (_constructionSiteUpgradeSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _constructionSiteUpgradeSelectionTilePrefab ");
        }
        if (_mainContentLabelPrefab == null)
        {
            Debug.LogError($"Could not find _mainContentLabelPrefab");
        }
        if (_workerSelectionTilePrefab == null)
        {
            Debug.LogError($"Could not find _workerSelectionTilePrefab");
        }

        Instance = this;
    }

    public GameObject GetStepLabelPrefab()
    {
        return _gameStepLabelPrefab;
    }

    public GameObject GetLocationSelectionTilePrefab()
    {
        return _locationSelectionTilePrefab;
    }

    public GameObject GetNextActionStepButtonPrefab()
    {
        return _nextStepButtonPrefab;
    }

    public GameObject GetPlayerSelectionTilePrefab()
    {
        return _playerSelectionTilePrefab;
    }

    public GameObject GetGameActionWindowPrefab()
    {
        return _gameActionWindowPrefab;
    }

    public GameObject GetActionSelectionTilePrefab()
    {
        return _actionSelectionTilePrefab;
    }

    public GameObject GetMainContentLabelPrefab()
    {
        return _mainContentLabelPrefab;
    }

    public GameObject GetConstructionSiteUpgradeSelectionTilePrefab()
    {
        return _constructionSiteUpgradeSelectionTilePrefab;
    }

    public GameObject GetWorkerSelectionTilePrefab()
    {
        return _workerSelectionTilePrefab;
    }

    public GameObject GetInputFieldPrefab()
    {
        return _inputFieldPrefab;
    }
}
