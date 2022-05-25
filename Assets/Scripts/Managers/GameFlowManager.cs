using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsible for arranging the next turn and text move procedures. Responsible for managing player actions, triggering random events etc.
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

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
    }
}
