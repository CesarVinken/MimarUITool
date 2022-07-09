public class UpgradeConstructionSiteGameActionHandler : IGameActionHandler
{
    public void Handle(GameActionCheckSum gameActionCheckSum)
    {
        UpgradeStockpile(gameActionCheckSum.Player);
    }

    private void UpgradeStockpile(Player player)
    {
        StockpileUpgrade stockpileUpgrade = player.StockpileMaximum.GetNextUpgrade();

        for (int i = 0; i < stockpileUpgrade.Costs.Count; i++)
        {
            player.AddResource(stockpileUpgrade.Costs[i].GetResourceType(), -stockpileUpgrade.Costs[i].Value);
        }

        player.StockpileMaximum.SetLevel(stockpileUpgrade);
    }
}