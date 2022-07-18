using UnityEngine;
public class WorkerGameActionHandler : IGameActionHandler
{
    private GameActionCheckSum _gameActionCheckSum;
    
    public void Handle(GameActionCheckSum gameActionCheckSum)
    {
        _gameActionCheckSum = gameActionCheckSum;

        WorkerGameAction workerGameAction = _gameActionCheckSum.GameAction as WorkerGameAction;
        WorkerActionType workerActionType = workerGameAction.GetWorkerActionType();
        IWorker worker = workerGameAction.GetWorker();

        switch (workerActionType)
        {
            case WorkerActionType.Bribe:
                BribeWorker(worker);
                break;
            case WorkerActionType.ExtendContract:
                int existingContractLength = worker.ServiceLength;
                int additionalContractLength = workerGameAction.GetContractLength();
                ExtendContract(worker, existingContractLength + additionalContractLength);
                break;
            case WorkerActionType.Hire:
                HireWorker(worker, workerGameAction.GetContractLength());
                break;
            default:
                new NotImplementedException("WorkerActionType", workerActionType.ToString());
                break;
        }
    }

    private void BribeWorker(IWorker worker)
    {
        //WorkerTile workerTile = worker.UIWorkerTile;
        //GameFlowManager.Instance.ExecuteHireWorkerEvent(_gameActionCheckSum.Player.PlayerNumber, worker);
        //switch (Worker.Employer)
        //{
        //    case PlayerNumber.Player1:
        //        break;
        //    case PlayerNumber.Player2:
        //        SetEmployer(PlayerNumber.Player3);
        //        break;
        //    case PlayerNumber.Player3:
        //        SetEmployer(PlayerNumber.None);
        //        break;
        //    case PlayerNumber.None:
        //        SetEmployer(PlayerNumber.Player1);
        //        UpdateServiceLength(3);
        //        break;
        //    default:
        //        break;
        //}
        //SetEmployer(PlayerNumber.Player2);

        //SetButtonColour(Worker.Employer);
    }

    private void ExtendContract(IWorker worker, int contractDuration)
    {
        Debug.LogWarning($"FULL contractDuration {contractDuration}");
        Player player = _gameActionCheckSum.Player;
        GameFlowManager.Instance.ExecuteExtendWorkerContractEvent(EventTriggerSourceType.GameAction, player.PlayerNumber, worker, contractDuration);
    }

    private void HireWorker(IWorker worker, int contractDuration)
    {
        Player player = _gameActionCheckSum.Player;
        GameFlowManager.Instance.ExecuteHireWorkerEvent(EventTriggerSourceType.GameAction, player.PlayerNumber, worker, contractDuration);
    }
}
