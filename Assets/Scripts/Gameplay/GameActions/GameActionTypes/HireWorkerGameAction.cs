
using System.Collections.Generic;

public class HireWorkerGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public HireWorkerGameAction()
    {
        _gameActionType = GameActionType.HireWorker;
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
