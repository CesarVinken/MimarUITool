public class GameActionCheckSum
{
    public Player Player { get; private set; }

    public IGameAction GameAction { get; private set; }

    public void WithPlayer(Player player)
    {
        Player = player;
    }

    public void WithActionType(IGameAction gameAction)
    {
        GameAction = gameAction;
    }
}
