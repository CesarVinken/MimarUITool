using UnityEngine;
public class Wood : IResource
{
    public Player Player { get; private set; }
    public int Amount { get; private set; }

    public Wood(int amount, Player player)
    {
        Amount = amount;
        Player = player;
    }
    public void SetAmount(int newAmount)
    {
        Amount = newAmount;
    }

    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public ResourceType GetResourceType()
    {
        return ResourceType.Wood;
    }

    public int GetAmountCap()
    {
        return Player.StockpileMaximum.Amount;
    }
}
