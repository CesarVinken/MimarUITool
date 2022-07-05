public class GameActionCheckSum
{
    public Player Player { get; private set; }

    public UIToolGameActionType ActionType { get; private set; }

    public void WithPlayer(Player player)
    {
        Player = player;
    }

    public void WithActionType(UIToolGameActionType actionType)
    {
        ActionType = actionType;
    }
}
