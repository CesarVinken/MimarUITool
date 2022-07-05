public class GameActionCheckSum
{
    public Player Player { get; private set; }

    public GameActionType ActionType { get; private set; }

    public void WithPlayer(Player player)
    {
        Player = player;
    }

    public void WithActionType(GameActionType actionType)
    {
        ActionType = actionType;
    }
}
