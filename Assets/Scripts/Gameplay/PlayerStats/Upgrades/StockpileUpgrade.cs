using System.Collections.Generic;
using UnityEngine;

public class StockpileUpgrade
{
    public UpgradeLevel UpgradeLevel { get; private set; }
    public int AmountCap { get; private set; }

    public List<IResource> Costs = new List<IResource>();

    public static StockpileUpgrade GetUpgradeByLevel(UpgradeLevel level)
    {
        return new StockpileUpgrade(level);
    }

    public StockpileUpgrade(UpgradeLevel upgradeLevel)
    {
        UpgradeLevel = upgradeLevel;

        InitialiseCosts();
        InitialiseAmountCap();
    }

    private void InitialiseCosts()
    {
        switch (UpgradeLevel)
        {
            case UpgradeLevel.Level0:
                Costs.Add(new Wood(30)); // TODO we need to reorganise this in order to make it possible to configure the values through files or live in the dashboard
                break;
            case UpgradeLevel.Level1:
                Costs.Add(new Wood(60));
                break;
            case UpgradeLevel.Level2:
                Costs.Add(new Wood(90));
                break;
            default:
                Debug.LogError($"There is no implementation for {UpgradeLevel}");
                break;
        }
    }

    private void InitialiseAmountCap()
    {
        switch (UpgradeLevel)
        {
            case UpgradeLevel.Level0:
                AmountCap = 60; // TODO we need to reorganise this in order to make it possible to configure the values through files or live in the dashboard
                break;
            case UpgradeLevel.Level1:
                AmountCap = 90;
                break;
            case UpgradeLevel.Level2:
                AmountCap = 120;
                break;
            default:
                Debug.LogError($"There is no implementation for {UpgradeLevel}");
                break;
        }
    }
}
