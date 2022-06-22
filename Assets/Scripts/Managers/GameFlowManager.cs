using System;

using UnityEngine;

// Responsible for arranging the next turn and text move procedures. Responsible for managing player actions, triggering random events etc.
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public event EventHandler<MonumentComponentCompletionStateChangeEvent> MonumentComponentCompletionStateChangeEvent;

    public void Setup()
    {
        Instance = this;
    }

    public void ExecuteNextGameStep()
    {
        // TODO: NextTurn() if no player has moved left, otherwise, NextMove()
        NextTurn();
    }

    private void NextTurn()
    {
        PlayerManager.Instance.PayIncomes();
        PlayerManager.Instance.CollectResources();
        PlayerManager.Instance.PerformBuildingTasks();
        PlayerManager.Instance.DistractWorkerServiceLength();
    }

    public void ExecuteMonumentComponentStateChangeEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, MonumentComponentState state)
    {
        MonumentComponentCompletionStateChangeEvent?.Invoke(this, new MonumentComponentCompletionStateChangeEvent(affectedPlayer, affectedComponent, state));
    }
}
