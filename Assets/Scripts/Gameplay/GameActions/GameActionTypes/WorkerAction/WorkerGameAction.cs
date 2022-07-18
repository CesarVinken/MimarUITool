
using System.Collections.Generic;

public class WorkerGameAction : IGameAction
{
    private GameActionType _gameActionType;
    private WorkerActionType _workerActionType;
    private int _contractLength;
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

    public void WithContractLength(int duration)
    {
        _contractLength = duration;
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

    public int GetContractLength()
    {
        return _contractLength;
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
