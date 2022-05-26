using UnityEngine;

public class PlayersTabContainer : UITabContainer
{
    [SerializeField] private PlayerSelectionButton _player1SelectionButton;
    [SerializeField] private PlayerSelectionButton _player2SelectionButton;
    [SerializeField] private PlayerSelectionButton _player3SelectionButton;

    [SerializeField] private PlayerUIContent _playerUIContentContainer;
    public PlayerSelectionButton CurrentPlayerTab { get; private set; } = null;

    private void Awake()
    {
        if (_player1SelectionButton == null)
        {
            Debug.LogError($"Cannot find _player1SelectionButton on {gameObject.name}");
        }
        if (_player2SelectionButton == null)
        {
            Debug.LogError($"Cannot find _player2SelectionButton on {gameObject.name}");
        }
        if (_player3SelectionButton == null)
        {
            Debug.LogError($"Cannot find _player3SelectionButton on {gameObject.name}");
        }

        if (_playerUIContentContainer == null)
        {
            Debug.LogError($"Cannot find _playerUIContentContainer on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _playerUIContentContainer.Initialise();
        _player1SelectionButton.Initialise(PlayerNumber.Player1);
        _player2SelectionButton.Initialise(PlayerNumber.Player2);
        _player3SelectionButton.Initialise(PlayerNumber.Player3);
        SetPlayerTab(_player1SelectionButton);
    }

    public void SetPlayerTab(PlayerSelectionButton newTab)
    {
        if (CurrentPlayerTab == null)
        {
            CurrentPlayerTab = newTab;
            CurrentPlayerTab.Activate();
            return;
        }

        if (CurrentPlayerTab == newTab)
        {
            CurrentPlayerTab.UpdatePlayerData();
            return;
        }

        CurrentPlayerTab.Deactivate();
        CurrentPlayerTab = newTab;
        CurrentPlayerTab.Activate();
    }

    public void FillInPlayerContent(UIPlayerData playerData)
    {
        _playerUIContentContainer.UpdatePlayerUIContent(playerData);
    }

    public override void Activate()
    {
        CurrentPlayerTab.UpdatePlayerData();
        gameObject.SetActive(true);
    }
}
