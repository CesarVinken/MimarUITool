using System;

using UnityEngine;

// Responsible for arranging the next turn and text move procedures. Responsible for managing player actions, triggering random events etc.
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public TimeOfDay TimeOfDay { get; private set; } = TimeOfDay.Morning;

    public event EventHandler<MonumentComponentCompletionStateChangeEvent> MonumentComponentCompletionStateChangeEvent;

    public void Setup()
    {
        Instance = this;

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.GetNextMoveButton().UpdateText();
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

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.GetNextMoveButton().UpdateText();
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
