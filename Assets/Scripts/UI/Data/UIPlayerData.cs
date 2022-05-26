using System.Collections.Generic;
using UnityEngine;

public class UIPlayerData
{
    public Player Player { get; private set; }
    //public string Name { get; private set; }
    //public int Gold { get; private set; }
    //public int Reputation { get; private set; }
    //public int StockpileMaximum { get; private set; }
    //public List<IResource> Resources { get; private set; }

    public UIPlayerData WithPlayer(Player player)
    {
        Player = player;
        return this;
    }

    //public UIPlayerData WithName(string name)
    //{
    //    Name = name;
    //    return this;
    //}

    //public UIPlayerData WithGold(int gold)
    //{
    //    Gold = gold;
    //    return this;
    //}

    //public UIPlayerData WithReputation(int reputation)
    //{
    //    Reputation = reputation;
    //    return this;
    //}

    //public UIPlayerData WithResources(List<IResource> resources)
    //{
    //    Resources = resources;
    //    return this;
    //}

    //public UIPlayerData WithStockpileMaximum(int maximum)
    //{
    //    StockpileMaximum = maximum;
    //    return this;
    //}
}
