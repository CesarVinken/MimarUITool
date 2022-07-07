using UnityEngine;

public class InitialisationManager : MonoBehaviour
{
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private NavigationManager _navigationManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private GameFlowManager _gameFlowManager;
    [SerializeField] private AssetManager _assetManager;

    void Awake()
    {
        if (_locationManager == null)
        {
            Debug.LogError($"Cannot find the location manager");
        }
        if (_navigationManager == null)
        {
            Debug.LogError($"Cannot find the navigation manager");
        }
        if (_playerManager == null)
        {
            Debug.LogError($"Cannot find the player manager");
        }
        if (_gameFlowManager == null)
        {
            Debug.LogError($"Cannot find the game flow manager");
        }
        if (_assetManager == null)
        {
            Debug.LogError($"Cannot find the asset manager");
        }

        // Set instances, check null references
        _navigationManager.Setup();
        _locationManager.Setup();
        _playerManager.Setup();
        _gameFlowManager.Setup();
        _assetManager.Setup();

        _playerManager.InitialisePlayers();
        _playerManager.InitialisePlayerPriority();

        // Initialise the UI components once all the data (player data, etc) is in place and ready to be represented.
        _navigationManager.InitialiseTabButtons();
        _navigationManager.InitialiseTabContainers();
        _navigationManager.InitialiseLabourPools();

        _playerManager.RefreshPlayerMoves();
    }

}
