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

    public void Setup()
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

        _monumentsDisplayContainer.Setup();

    }

    public void Initialise()
    {
        _player1SelectionButton.Initialise(PlayerNumber.Player1);
        _player2SelectionButton.Initialise(PlayerNumber.Player2);
        _player3SelectionButton.Initialise(PlayerNumber.Player3);
        _playerUIContentContainer.Initialise(this);

        _monumentsDisplayContainer.Initialise(); // initialise all the 3d models for the monument (async)
    }

    public void OnInitialisationFinished()
    {
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

    public MonumentComponentState HandleMonumentComponentState(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        Player currentPlayer = PlayerManager.Instance.Players[CurrentPlayerTab.PlayerNumber];
        Monument monument = currentPlayer.Monument;

        MonumentComponent monumentComponent = monument.GetMonumentComponentByType(monumentComponentBlueprint.MonumentComponentType);
        MonumentComponentState newState = GetNextMonumentComponentStateForClick(monumentComponent.State);


        monument.SetMonumentComponentState(monumentComponentBlueprint.MonumentComponentType, newState);

        if (newState == MonumentComponentState.Complete)
        {
            _monumentsDisplayContainer.SetMonumentComponentVisibility(CurrentPlayerTab.PlayerNumber, monumentComponent, MonumentComponentVisibility.Complete);
        }
        else if(newState == MonumentComponentState.InProgress)
        {
            _monumentsDisplayContainer.SetMonumentComponentVisibility(CurrentPlayerTab.PlayerNumber, monumentComponent, MonumentComponentVisibility.InProgress);
        }
        else
        {
            _monumentsDisplayContainer.SetMonumentComponentVisibility(CurrentPlayerTab.PlayerNumber, monumentComponent, MonumentComponentVisibility.Hidden);
        }

        GameFlowManager.Instance.ExecuteMonumentComponentStateChangeEvent(currentPlayer.PlayerNumber, monumentComponent, newState);

        return newState;

    }

    private MonumentComponentState GetNextMonumentComponentStateForClick(MonumentComponentState currentState)
    {
        if (currentState == MonumentComponentState.InProgress)
        {
            return MonumentComponentState.Complete;
        }
        else if (currentState == MonumentComponentState.Complete)
        {
            return MonumentComponentState.Buildable; //MAYBE we need to check here if is should be buildable/unaffordable/locked
        }

        return MonumentComponentState.InProgress;
    }

    public void UpdatePlayerStatUIContent(UIPlayerData playerData)
    {
        _playerUIContentContainer.UpdatePlayerUIContent(playerData);
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
        CurrentPlayerTab?.UpdatePlayerDataDisplay();
        gameObject.SetActive(true);
    }
}
