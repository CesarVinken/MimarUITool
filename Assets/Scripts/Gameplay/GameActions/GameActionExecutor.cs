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
            HandlePlannedGameAction(gameActionsOrderedByPriority[i]);
        }

        PlayerManager.Instance.RefreshPlayerMoves();
    }

    private void HandlePlannedGameAction(GameActionCheckSum gameActionCheckSum)
    {
        GameActionType gameActionType = gameActionCheckSum.GameAction.GetGameActionType();
        PlayerManager.Instance.UpdatePlayerMove(gameActionCheckSum.Player, false);
        Player player = gameActionCheckSum.Player;

        switch (gameActionType)
        {
            case GameActionType.HireWorker:
                Debug.Log($"EXECUTE ACTION ---- Hire a worker for {gameActionCheckSum.Player.Name}");
                break;
            case GameActionType.UpgradeConstructionSite:
                Debug.Log($"Upgrade the construction site for {gameActionCheckSum.Player.Name}");
                ConstructionSiteUpgradeHandler constructionSiteUpgradeHandler = new ConstructionSiteUpgradeHandler();
                constructionSiteUpgradeHandler.UpgradeStockpile(player);
                break;
            default:
                Debug.LogError($"The game action type {gameActionCheckSum.GameAction.GetName()} is not implemented");
                break;
        }
    }
}

