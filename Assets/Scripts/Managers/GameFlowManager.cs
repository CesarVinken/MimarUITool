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
    private GameActionPaymentHandler _gameActionPaymentHandler;

    public event EventHandler<MonumentComponentCompletionStateChangeEvent> MonumentComponentCompletionStateChangeEvent;
    public event EventHandler<HireWorkerEvent> HireWorkerEvent;
    public event EventHandler<ExtendWorkerContractEvent> ExtendWorkerContractEvent;
    public event EventHandler<BribeWorkerEvent> BribeWorkerEvent;

    public void Setup()
    {
        Instance = this;

        _gameActionExecutor = new GameActionExecutor();
        _gameActionPaymentHandler = new GameActionPaymentHandler();
        _gameActionPaymentHandler.Initialise();

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

    public void ExecuteHireWorkerEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber employer, IWorker worker, int contractLength)
    {
        Debug.LogWarning($"We will evoke the HIRE WORKER EVENT");
        HireWorkerEvent?.Invoke(this, new HireWorkerEvent(eventTriggerSourceType, employer, worker, contractLength));
    }

    public void ExecuteExtendWorkerContractEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber employer, IWorker worker, int contractLength)
    {
        Debug.LogWarning($"We will evoke the EXTEND WORKER CONTRACT EVENT");
        ExtendWorkerContractEvent?.Invoke(this, new ExtendWorkerContractEvent(eventTriggerSourceType, employer, worker, contractLength));
    }

    public void ExecuteBribeWorkerEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber employer, IWorker worker)
    {
        Debug.LogWarning($"We will evoke the BRIBE WORKER EVENT");
        BribeWorkerEvent?.Invoke(this, new BribeWorkerEvent(eventTriggerSourceType, employer, worker));
    }
}
