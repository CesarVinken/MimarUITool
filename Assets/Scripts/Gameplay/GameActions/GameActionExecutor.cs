using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameActionExecutor
{
    public void HandlePlannedGameActions(List<GameActionCheckSum> plannedGameActions)
    {
        List<GameActionCheckSum> gameActionsOrderedByPriority = plannedGameActions.OrderBy(a => PlayerManager.Instance.PlayersByPriority.IndexOf(a.Player)).ToList();

        // execute actions in order of priority
        for (int i = 0; i < gameActionsOrderedByPriority.Count; i++)
        {
            Debug.LogWarning($"{i} in order is {gameActionsOrderedByPriority[i].Player.Name}");
            HandlePlannedGameAction(gameActionsOrderedByPriority[i]);
        }

        PlayerManager.Instance.RefreshPlayerMoves();
    }

    private void HandlePlannedGameAction(GameActionCheckSum gameActionCheckSum)
    {
        GameActionType gameActionType = gameActionCheckSum.ActionType;
        PlayerManager.Instance.UpdatePlayerMove(gameActionCheckSum.Player, false);

        switch (gameActionType)
        {
            case GameActionType.HireWorker:
                Debug.Log($"EXECUTE ACTION ---- Hire a worker for {gameActionCheckSum.Player.Name}");
                break;
            case GameActionType.ExpandStockpile:
                Debug.Log($"Expand the stockpile for {gameActionCheckSum.Player.Name}");
                break;
            default:

                Debug.LogError($"The game action type {gameActionType} is not implemented");
                break;
        }
    }
}