using System.Collections;
using System.Collections.Generic;

public class MakeSacrificeGameAction : IGameAction
{
    public List<IAccumulativePlayerStat> GetCosts()
    {
        return TempConfiguration.MakeSacrificeTransaction;
    }

    public GameActionType GetGameActionType()
    {
        return GameActionType.MakeSacrifice;
    }

    public string GetName()
    {
        return "Make sacrifice";
    }

    public bool IsAvailableForPlayer(Player player)
    {
        return true;
    }
}
