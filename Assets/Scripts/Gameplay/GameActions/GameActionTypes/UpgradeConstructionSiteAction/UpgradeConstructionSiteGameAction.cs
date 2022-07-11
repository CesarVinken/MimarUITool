using System.Collections.Generic;
using UnityEngine;
public class UpgradeConstructionSiteGameAction : IGameAction
{
    private GameActionType _gameActionType;
    private IConstructionSiteUpgrade _plannedUpgrade;

    public UpgradeConstructionSiteGameAction()
    {
        _gameActionType = GameActionType.UpgradeConstructionSite;
        _plannedUpgrade = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player.StockpileMaximum.GetNextUpgrade();
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
        //_plannedUpgrade = player.StockpileMaximum.GetNextUpgrade();

        // check if player is at maximum upgrade level
        UpgradeLevel highestLevel = player.StockpileMaximum.MaximumUpgrade;
        int currentCap = player.StockpileMaximum.Value;
        int highestLevelCap = new StockpileUpgrade(highestLevel).AmountCap;

        if (currentCap >= highestLevelCap) return false;

        // check if player has resources to pay material costs

        for (int i = 0; i < _plannedUpgrade.Costs.Count; i++)
        {
            IAccumulativePlayerStat cost = _plannedUpgrade.Costs[i];
            IResource resource = cost as IResource;
            if (cost != null && player.Resources[resource.GetResourceType()].Value < resource.Value)
            {
                return false;
            }
        }

        // check if player has money to travel to construction site (if needed)
        if(player.Gold.Value == 0 && player.Location.LocationType != player.Monument.ConstructionSite)
        {
            return false;
        }

        return true;
    }

    public List<IAccumulativePlayerStat> GetCosts()
    {
        return _plannedUpgrade.Costs;
    }
}
