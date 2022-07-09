

public class TravelAction : IGameAction
{
    private GameActionType _gameActionType;

    public TravelAction()
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
}
