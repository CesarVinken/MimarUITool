using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameActionExecutor
{
    private List<IGameActionTarget> _possibleOverlappingTargets = new List<IGameActionTarget>();

    public void HandlePlannedGameActions(List<GameActionCheckSum> plannedGameActions)
    {
        _possibleOverlappingTargets.Clear();
        List<GameActionCheckSum> gameActionsOrderedByPriority = plannedGameActions.OrderBy(a => PlayerManager.Instance.PlayersByPriority.IndexOf(a.Player)).ToList();

        // execute actions in order of priority
        for (int i = 0; i < gameActionsOrderedByPriority.Count; i++)
        {
            GameActionCheckSum checkSum = gameActionsOrderedByPriority[i];
            bool actionSuccess = ActionTargetIsStillUnaffected(checkSum);
            checkSum.WithActionSuccess(actionSuccess);

            HandlePlannedGameAction(gameActionsOrderedByPriority[i]);
        }

        PlayerManager.Instance.RefreshPlayerMoves();
    }

        // With some action, players can have the same target.
        // In this case, the player with the highest priority wins.
        // The losing player should only move to location (if necessary), but not execute any other part of the GameAction
    private bool ActionTargetIsStillUnaffected(GameActionCheckSum checkSum)
    {
        GameActionType gameActionType = checkSum.GameAction.GetGameActionType();

        switch (gameActionType)
        {
            case GameActionType.ManageWorker:
                WorkerGameAction workerGameAction = checkSum.GameAction as WorkerGameAction;
                IWorker targetWorker = workerGameAction.GetWorker();
                
                for (int i = 0; i < _possibleOverlappingTargets.Count; i++)
                {
                    Debug.LogWarning($"Going over {_possibleOverlappingTargets[i]}");
                    if(_possibleOverlappingTargets[i] == targetWorker)
                    {
                        // If there is an overlap for a neutral worker, try to hire another neutral worker from the same labour pool.
                        // If there is none, apply normal priority rules
                        if (workerGameAction.GetWorkerActionType() == WorkerActionType.Hire)
                        {
                            ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(checkSum.Location.LocationType);
                            List<IWorker> neutralLabourWorkers = labourPoolLocation.GetLabourPoolWorkers().Where(w => w.Employer == PlayerNumber.None).ToList();

                            for (int j = 0; j < neutralLabourWorkers.Count; j++)
                            {
                                IWorker neutralWorker = neutralLabourWorkers[j];

                                if (neutralWorker == targetWorker) continue;
                                if (_possibleOverlappingTargets.Contains(neutralWorker)) continue;

                                _possibleOverlappingTargets.Add(neutralWorker);
                                workerGameAction.WithWorker(neutralWorker);
                                return true;
                            }
                        }
                        return false;
                    }
                }
                return true;
            case GameActionType.MakeSacrifice:
            case GameActionType.Travel:
            case GameActionType.UpgradeConstructionSite:
                return true;
            default:
                new NotImplementedException("GameActionType", gameActionType.ToString());
                return true;
        }
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

                WorkerGameAction workerGameAction = gameActionCheckSum.GameAction as WorkerGameAction;
                IWorker targetWorker = workerGameAction.GetWorker();

                _possibleOverlappingTargets.Add(targetWorker);

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
            case GameActionType.MakeSacrifice:
                Debug.Log($"Make a sacrifice for {gameActionCheckSum.Player.Name}");

                MakeSacrificeGameActionHandler makeSacrificeGameActionHandler = new MakeSacrificeGameActionHandler();
                makeSacrificeGameActionHandler.Handle(gameActionCheckSum);
                break;
            default:
                new NotImplementedException("GameAction", gameActionCheckSum.GameAction.GetName());
                break;
        }
    }
}
