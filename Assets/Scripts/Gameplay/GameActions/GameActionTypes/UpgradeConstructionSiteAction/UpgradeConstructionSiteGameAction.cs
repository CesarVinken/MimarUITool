using System.Collections.Generic;
using UnityEngine;
public class UpgradeConstructionSiteGameAction : IGameAction
{
    private GameActionType _gameActionType;
    private IConstructionSiteUpgrade _plannedUpgrade;
    private ConstructionSiteUpgradeType _constructionSiteUpgradeType;

    public UpgradeConstructionSiteGameAction()
    {
        _gameActionType = GameActionType.UpgradeConstructionSite;
    }

    public void WithUpgradeType(ConstructionSiteUpgradeType constructionSiteUpgradeType)
    {
        _constructionSiteUpgradeType = constructionSiteUpgradeType;

        switch (_constructionSiteUpgradeType)
        {
            case ConstructionSiteUpgradeType.StockpileMaximum:
                _plannedUpgrade = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player.StockpileMaximum.GetNextUpgrade();
                break;
            default:
                new NotImplementedException("ConstructionSiteUpgradeType", _constructionSiteUpgradeType.ToString());
                break;
        }

    }

    public string GetName()
    {
        return "Upgrade Construction Site";
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }

    public ConstructionSiteUpgradeType GetConstructionSiteUpgradeType()
    {
        return _constructionSiteUpgradeType;
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

        // TODO: Make a list of all available upgrades to check if the player can afford at least one of them. Currently we only check the next stockpile upgrade
        IConstructionSiteUpgrade stockpileUpgrade = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player.StockpileMaximum.GetNextUpgrade();

        for (int i = 0; i < stockpileUpgrade.Costs.Count; i++)
        {
            IAccumulativePlayerStat cost = stockpileUpgrade.Costs[i];
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
