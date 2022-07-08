using System.Collections.Generic;
using UnityEngine;

public class StockpileMaximum : ISingleValuedPlayerStat
{
    public Player Player { get; private set; }
    public int Value { get; private set; }
    private UpgradeLevel _level = UpgradeLevel.Level0;

    public UpgradeLevel MaximumUpgrade { get; private set; } = UpgradeLevel.Level2;

    public StockpileMaximum(Player player)
    {
        Player = player;
    }

    public void SetLevel(StockpileUpgrade stockpileUpgrade)
    {
        _level = stockpileUpgrade.UpgradeLevel;
        Value = stockpileUpgrade.AmountCap;
    }

    public StockpileUpgrade GetNextUpgrade()
    {
        if(_level == UpgradeLevel.Level0)
        {
            return new StockpileUpgrade(UpgradeLevel.Level1);
        }
        else if(_level == UpgradeLevel.Level1)
        {
            return new StockpileUpgrade(UpgradeLevel.Level2);
        }
        else
        {
            return null;
        }
    }
}


