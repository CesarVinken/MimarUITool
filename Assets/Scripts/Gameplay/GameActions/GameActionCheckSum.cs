public class GameActionCheckSum
{
    public Player Player { get; private set; }

    public IGameAction GameAction { get; private set; }
    public ILocation Location { get; private set; }

    public void WithPlayer(Player player)
    {
        Player = player;
    }

    public void WithActionType(IGameAction gameAction)
    {
        GameAction = gameAction;
    }

    public void WithLocation(ILocation location)
    {
        Location = location;
    }
}
