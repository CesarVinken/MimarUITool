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
        MonumentComponentState oldState = monumentComponent.State;
        MonumentComponentState newState = GetNextMonumentComponentStateForClick(monumentComponent.State, monumentComponent);

        Debug.Log($"The new state is {newState}");
        monument.SetMonumentComponentState(monumentComponentBlueprint.MonumentComponentType, newState);

        // things might update with dependencies, update UI items and 3d model visibility
        if (newState == MonumentComponentState.Complete ||
            oldState == MonumentComponentState.Complete)
        {
            monument.UpdateDependencies();
            _monumentUIContainer.UpdateUIForItems(monument);
            _monumentsDisplayContainer.UpdateVisibilityForComponents(monument);
        }
        else
        {
            _monumentsDisplayContainer.UpdateVisibilityForComponent(monumentComponent);
        }

        GameFlowManager.Instance.ExecuteMonumentComponentStateChangeEvent(currentPlayer.PlayerNumber, monumentComponent, newState);

        return newState;

    }

    private MonumentComponentState GetNextMonumentComponentStateForClick(MonumentComponentState currentState, MonumentComponent monumentComponent)
    {
        Debug.Log($"REEVALUATE. CURRENT STATE IS {currentState}");
        if (currentState == MonumentComponentState.InProgress)
        {
            return MonumentComponentState.Complete;
        }
        else if (currentState == MonumentComponentState.Complete)
        {
            Debug.Log($"alREADY COMPLETE");
            return GetBuildableMonumentComponentState(monumentComponent);
        }

        return MonumentComponentState.InProgress;
    }

    public MonumentComponentState GetBuildableMonumentComponentState(MonumentComponent monumentComponent)
    {
        Player player = PlayerManager.Instance.Players[monumentComponent.PlayerNumber];
        bool canAffordCost = PlayerUtility.CanAffordCost(
            monumentComponent.MonumentComponentBlueprint.ResourceCosts,
            player.Resources
            );
        Debug.Log($"aASDASD");

        if (canAffordCost)
        {
            return MonumentComponentState.Buildable;
        }

        return MonumentComponentState.Unaffordable;
    }

    public void UpdatePlayerStatUIContent(UIPlayerData playerData)
    {
        _playerUIContentContainer.UpdatePlayerUIContent(playerData);
    }

    // Update the list items
    public void UpdateMonumentUI()
    {
        Player currentPlayer = PlayerManager.Instance.Players[CurrentPlayerTab.PlayerNumber];
        Monument monument = currentPlayer.Monument;

        _monumentUIContainer.UpdateUIForItems(monument);
    }

    // Update the 3d model of the monument
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
