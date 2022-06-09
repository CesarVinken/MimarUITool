using UnityEngine;

public class PlayersTabContainer : UITabContainer
{
    [SerializeField] private PlayerSelectionButton _player1SelectionButton;
    [SerializeField] private PlayerSelectionButton _player2SelectionButton;
    [SerializeField] private PlayerSelectionButton _player3SelectionButton;

    [SerializeField] private PlayerUIContent _playerUIContentContainer;
    [SerializeField] private MonumentsDisplayContainer _monumentsDisplayContainer;
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
        if (_monumentsDisplayContainer == null)
        {
            Debug.LogError($"Cannot find _monumentsDisplayContainer on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _playerUIContentContainer.Initialise(this);
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

    public bool HandleMonumentComponentCompletion(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        Player currentPlayer = PlayerManager.Instance.Players[CurrentPlayerTab.PlayerNumber];
        //bool isCompleted = currentPlayer.HandleMonumentComponentCompletion(monumentComponentBlueprint);
        Monument monument = currentPlayer.Monument;

        MonumentComponent monumentComponent = monument.GetMonumentComponentByType(monumentComponentBlueprint.MonumentComponentType);
        monument.SetMonumentComponentCompletion(monumentComponentBlueprint.MonumentComponentType, !monumentComponent.Complete);

        bool componentIsComplete = monumentComponent.Complete;

        if (componentIsComplete)
        {
            _monumentsDisplayContainer.ShowMonumentComponent(CurrentPlayerTab.PlayerNumber, monumentComponent);
        }
        else
        {
            _monumentsDisplayContainer.HideMonumentComponent(CurrentPlayerTab.PlayerNumber, monumentComponent);
        }

        // update monument visuals
        // update workers
        // update component remaining labour time
        return componentIsComplete;

    }

    public void UpdateMonumentDisplay()
    {
        _monumentsDisplayContainer.ShowMonument(CurrentPlayerTab.PlayerNumber);
    }

    public override void Activate()
    {
        CurrentPlayerTab.UpdatePlayerData();
        gameObject.SetActive(true);
    }
}
