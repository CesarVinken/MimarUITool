
public interface IGameAction
{
    string GetName();
    GameActionType GetGameActionType();
    bool IsAvailableForPlayer(Player player);
}
