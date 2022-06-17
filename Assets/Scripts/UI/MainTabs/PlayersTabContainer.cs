using System;
using UnityEngine;

public class PlayersTabContainer : UITabContainer
{
    public override MainTabType MainTabType { get; } = MainTabType.PlayersTab;

    [SerializeField] private PlayerSelectionButton _player1SelectionButton;
    [SerializeField] private PlayerSelectionButton _player2SelectionButton;
    [SerializeField] private PlayerSelectionButton _player3SelectionButton;

    [SerializeField] private PlayerUIContent _playerUIContentContainer;
    [SerializeField] private MonumentsDisplayContainer _monumentsDisplayContainer;
    [SerializeField] private MonumentUIContainer _monumentUIContainer;
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
        if (_monumentUIContainer == null)
        {
            Debug.LogError($"Cannot find _monumentUIContainer on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _player1SelectionButton.Initialise(PlayerNumber.Player1);
        _player2SelectionButton.Initialise(PlayerNumber.Player2);
        _player3SelectionButton.Initialise(PlayerNumber.Player3);
        _playerUIContentContainer.Initialise(this);

        _monumentsDisplayContainer.Initialise();
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
            CurrentPlayerTab.UpdatePlayerDataDisplay();
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
        Monument monument = currentPlayer.Monument;

        MonumentComponent monumentComponent = monument.GetMonumentComponentByType(monumentComponentBlueprint.MonumentComponentType);
        monument.SetMonumentComponentCompletion(monumentComponentBlueprint.MonumentComponentType, !monumentComponent.IsComplete);

        bool componentIsComplete = monumentComponent.IsComplete;

        if (componentIsComplete)
        {
            _monumentsDisplayContainer.ShowMonumentComponent(CurrentPlayerTab.PlayerNumber, monumentComponent);
        }
        else
        {
            _monumentsDisplayContainer.HideMonumentComponent(CurrentPlayerTab.PlayerNumber, monumentComponent);
        }

        GameFlowManager.Instance.ExecuteMonumentComponentCompletionEvent(currentPlayer.PlayerNumber, monumentComponent, componentIsComplete);

        // update monument visuals
        // update workers
        // update component remaining labour time
        return componentIsComplete;

    }

    public void UpdateMonumentUI()
    {
        Player currentPlayer = PlayerManager.Instance.Players[CurrentPlayerTab.PlayerNumber];
        Monument monument = currentPlayer.Monument;

        _monumentUIContainer.UpdateUIForItems(monument);
    }

    public void UpdateMonumentDisplay()
    {
        _monumentsDisplayContainer.ShowMonument(CurrentPlayerTab.PlayerNumber);
    }

    public override void Activate()
    {
        CurrentPlayerTab.UpdatePlayerDataDisplay();
        gameObject.SetActive(true);
    }
}
