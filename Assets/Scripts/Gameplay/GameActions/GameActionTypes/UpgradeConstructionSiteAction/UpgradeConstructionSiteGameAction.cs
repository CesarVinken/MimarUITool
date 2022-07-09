using UnityEngine;
public class UpgradeConstructionSiteGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public UpgradeConstructionSiteGameAction()
    {
        _gameActionType = GameActionType.UpgradeConstructionSite;
    }

    public string GetName()
    {
        return "Upgrade Construction Site";
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }

    public bool IsAvailableForPlayer(Player player)
    {
        StockpileUpgrade nextUpgrade = player.StockpileMaximum.GetNextUpgrade();

        // check if player is at maximum upgrade level
        UpgradeLevel highestLevel = player.StockpileMaximum.MaximumUpgrade;
        int currentCap = player.StockpileMaximum.Value;
        int highestLevelCap = new StockpileUpgrade(highestLevel).AmountCap;

        if (currentCap >= highestLevelCap) return false;

        // check if player has resources to pay material costs

        for (int i = 0; i < nextUpgrade.Costs.Count; i++)
        {
            IResource resource = nextUpgrade.Costs[i];
            if(player.Resources[resource.GetResourceType()].Value < resource.Value)
            {
                return false;
            }
        }

        // check if player has money to travel to construction site (if needed)

        // TODO

        return true;
    }
}
