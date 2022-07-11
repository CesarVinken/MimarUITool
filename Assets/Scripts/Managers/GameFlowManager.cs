using System;
using System.Collections.Generic;
using UnityEngine;

// Responsible for arranging the next turn and text move procedures. Responsible for managing player actions, triggering random events etc.
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public TimeOfDay TimeOfDay { get; private set; } = TimeOfDay.Morning;

    private List<GameActionCheckSum> _plannedGameActions = new List<GameActionCheckSum>();
    private GameActionExecutor _gameActionExecutor;

    public event EventHandler<MonumentComponentCompletionStateChangeEvent> MonumentComponentCompletionStateChangeEvent;

    public void Setup()
    {
        Instance = this;

        _gameActionExecutor = new GameActionExecutor();

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.GetNextMoveButton().UpdateText();

    }

    public void AddPlannedGameAction(GameActionCheckSum gameAction)
    {
        _plannedGameActions.Add(gameAction);
    }

    public void ExecuteNextGameStep()
    {
        if(TimeOfDay == TimeOfDay.Afternoon)
        {
            NextDay();
        }
        else
        {
            NextDayPart();
        }

        _gameActionExecutor.HandlePlannedGameActions(_plannedGameActions);
        _plannedGameActions.Clear();

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.GetNextMoveButton().UpdateText();
        gameTabContainer.UpdatePlayerPriorityUI();
    }

    private void NextDayPart()
    {
        if(TimeOfDay == TimeOfDay.Morning)
        {
            TimeOfDay = TimeOfDay.Noon;
        }
        else if(TimeOfDay == TimeOfDay.Noon)
        {
            TimeOfDay = TimeOfDay.Afternoon;
        }
        else
        {
            TimeOfDay = TimeOfDay.Morning;
        }
    }

    private void NextDay()
    {
        PlayerManager.Instance.PayIncomes();
        PlayerManager.Instance.CollectResources();
        PlayerManager.Instance.PerformBuildingTasks();
        PlayerManager.Instance.DistractWorkerServiceLength();

        TimeOfDay = TimeOfDay.Morning;
    }

    public void ExecuteMonumentComponentStateChangeEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, MonumentComponentState state)
    {
        MonumentComponentCompletionStateChangeEvent?.Invoke(this, new MonumentComponentCompletionStateChangeEvent(affectedPlayer, affectedComponent, state));
    }
}
