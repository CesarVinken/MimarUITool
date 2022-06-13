using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for arranging the next turn and text move procedures. Responsible for managing player actions, triggering random events etc.
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public event EventHandler<MonumentComponentCompletionStateChangeEvent> MonumentComponentCompletionStateChangeEvent;
    //public event Action OnBla;

    private void Awake()
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

    public void ExecuteMonumentComponentCompletionEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, bool isCompleted)
    {
        MonumentComponentCompletionStateChangeEvent?.Invoke(this, new MonumentComponentCompletionStateChangeEvent(affectedPlayer, affectedComponent, isCompleted));
    }
}
