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

        switch (gameActionType)
        {
            case GameActionType.ManageWorker:
                Debug.Log($"EXECUTE ACTION ---- Hire a worker for {gameActionCheckSum.Player.Name}");
                WorkerGameActionHandler workerGameActionHandler = new WorkerGameActionHandler();
                workerGameActionHandler.Handle(gameActionCheckSum);
                break;
            case GameActionType.Travel:
                Debug.Log($"EXECUTE ACTION ---- {gameActionCheckSum.Player.Name} travels to {gameActionCheckSum.Location.Name}");
                TravelGameActionHandler travelGameActionHandler = new TravelGameActionHandler();
                travelGameActionHandler.Handle(gameActionCheckSum);
                break;
            case GameActionType.UpgradeConstructionSite:
                Debug.Log($"Upgrade the construction site for {gameActionCheckSum.Player.Name}");
                UpgradeConstructionSiteGameActionHandler constructionSiteUpgradeHandler = new UpgradeConstructionSiteGameActionHandler();
                constructionSiteUpgradeHandler.Handle(gameActionCheckSum);
                break;
            default:
                new NotImplementedException("GameAction", gameActionCheckSum.GameAction.GetName());
                break;
        }
    }
}

