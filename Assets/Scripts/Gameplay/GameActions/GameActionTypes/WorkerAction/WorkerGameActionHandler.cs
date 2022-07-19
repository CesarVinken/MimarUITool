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
        Player player = _gameActionCheckSum.Player;
        GameFlowManager.Instance.ExecuteBribeWorkerEvent(EventTriggerSourceType.GameAction, player.PlayerNumber, worker);
    }

    private void ExtendContract(IWorker worker, int contractDuration)
    {
        Player player = _gameActionCheckSum.Player;
        GameFlowManager.Instance.ExecuteExtendWorkerContractEvent(EventTriggerSourceType.GameAction, player.PlayerNumber, worker, contractDuration);
    }

    private void HireWorker(IWorker worker, int contractDuration)
    {
        Player player = _gameActionCheckSum.Player;
        GameFlowManager.Instance.ExecuteHireWorkerEvent(EventTriggerSourceType.GameAction, player.PlayerNumber, worker, contractDuration);
    }
}
