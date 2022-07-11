
using System.Collections.Generic;

public interface IGameAction
{
    string GetName();
    GameActionType GetGameActionType();
    bool IsAvailableForPlayer(Player player);

    List<IAccumulativePlayerStat> GetCosts();

}
