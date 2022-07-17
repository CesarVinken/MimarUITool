using System.Collections.Generic;
using UnityEngine;
public class UpgradeConstructionSiteGameActionHandler : IGameActionHandler
{
    private GameActionCheckSum _gameActionCheckSum;
    public void Handle(GameActionCheckSum gameActionCheckSum)
    {
        _gameActionCheckSum = gameActionCheckSum;

        UpgradeConstructionSiteGameAction upgradeConstructionSiteGameAction = gameActionCheckSum.GameAction as UpgradeConstructionSiteGameAction;

        switch (upgradeConstructionSiteGameAction.GetConstructionSiteUpgradeType())
        {
            case ConstructionSiteUpgradeType.StockpileMaximum:
                UpgradeStockpile(gameActionCheckSum.Player);
                break;
            default:
                new NotImplementedException("ConstructionSiteUpgradeType", upgradeConstructionSiteGameAction.GetConstructionSiteUpgradeType().ToString());
                break;
        }
    }

    private void UpgradeStockpile(Player player)
    {
        StockpileUpgrade stockpileUpgrade = player.StockpileMaximum.GetNextUpgrade();

        HandleUpgradeCosts(player, stockpileUpgrade.Costs);
        HandleTravelling();

        player.StockpileMaximum.SetLevel(stockpileUpgrade);
    }

    private void HandleUpgradeCosts(Player player, List<IAccumulativePlayerStat> costs)
    {
        for (int i = 0; i < costs.Count; i++)
        {
            IResource resource = costs[i] as IResource;
            if (resource != null)
            {
                player.AddResource(resource.GetResourceType(), -resource.Value);
            }
        }
    }

    private void HandleTravelling()
    {
        ILocation constructionSiteLocation = _gameActionCheckSum.Location;

        if (constructionSiteLocation.LocationType == _gameActionCheckSum.Player.Location.LocationType) return; // If the player is already at the location, don't travel, don't subtract costs

        Player player = _gameActionCheckSum.Player;
        PlayerManager.Instance.GoToLocation(player, constructionSiteLocation.LocationType);
        player.SetGold(player.Gold.Value - 1);
    }

}