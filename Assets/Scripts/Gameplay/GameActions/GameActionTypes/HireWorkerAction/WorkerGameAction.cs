
using System.Collections.Generic;

public class WorkerGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public WorkerGameAction()
    {
        _gameActionType = GameActionType.ManageWorker;
    }
    public string GetName()
    {
        return "Hire worker";
    }


    public GameActionType GetGameActionType()
    {
        return _gameActionType;
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
