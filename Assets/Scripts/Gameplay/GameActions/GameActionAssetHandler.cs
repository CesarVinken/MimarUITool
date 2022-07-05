
using UnityEngine;

public class GameActionAssetHandler : MonoBehaviour
{
    public static GameActionAssetHandler Instance;

    [SerializeField] private GameObject _gameStepLabelPrefab;
    [SerializeField] private GameObject _gameActionButtonPrefab;
    [SerializeField] private GameObject _gameActionWindowPrefab;
    [SerializeField] private GameObject _playerSelectionTilePrefab;
    [SerializeField] private GameObject _actionSelectionTilePrefab;
    [SerializeField] private GameObject _mainContentLabelPrefab;

    private void Awake()
    {
        if (_gameStepLabelPrefab == null)
        {
            Debug.LogError($"Could not find _gameStepLabelPrefab");
        }
        if (_gameActionButtonPrefab == null)
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
        if (_mainContentLabelPrefab == null)
        {
            Debug.LogError($"Could not find _mainContentLabelPrefab");
        }

        Instance = this;
    }

    public GameObject GetStepLabelPrefab()
    {
        return _gameStepLabelPrefab;
    }
    public GameObject GetNextActionStepButtonPrefab()
    {
        return _gameActionButtonPrefab;
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
}
