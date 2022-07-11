

using System.Collections.Generic;

public class TravelGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public TravelGameAction()
    {
        _gameActionType = GameActionType.Travel ;
    }
    public string GetName()
    {
        return "Travel to location";
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
