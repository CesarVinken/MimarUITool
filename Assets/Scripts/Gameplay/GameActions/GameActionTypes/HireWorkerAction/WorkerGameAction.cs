
using System.Collections.Generic;

public class WorkerGameAction : IGameAction
{
    private GameActionType _gameActionType;
    private WorkerActionType _workerActionType;
    private int _contractDuration;
    private IWorker _worker;

    public WorkerGameAction()
    {
        _gameActionType = GameActionType.ManageWorker;
    }

    public void WithWorkerActionType(WorkerActionType workerActionType)
    {
        _workerActionType = workerActionType;
    }

    public void WithWorker(IWorker worker)
    {
        _worker = worker;
    }

    public void WithContractDuration(int duration)
    {
        _contractDuration = duration;
    }

    public string GetName()
    {
        return "Hire worker";
    }

    public IWorker GetWorker()
    {
        return _worker;
    }


    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }

    public WorkerActionType GetWorkerActionType()
    {
        return _workerActionType;
    }

    public int GetContractDuration()
    {
        return _contractDuration;
    }

    public bool IsAvailableForPlayer(Player player)
    {
        return true;
    }

    public List<IAccumulativePlayerStat> GetCosts()
    {
        return new List<IAccumulativePlayerStat>() { };
    }
}
