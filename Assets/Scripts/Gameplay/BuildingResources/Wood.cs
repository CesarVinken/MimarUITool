using UnityEngine;
public class Wood : IResource
{
    public Player Player { get; private set; }
    public int Value { get; private set; }

    public Wood(int amount, Player player)
    {
        Value = amount;
        Player = player;
    }

    public Wood(int amount)
    {
        Value = amount;
    }

    public void SetValue(int newAmount)
    {
        Value = newAmount;
    }

    public void AddValue(int amount)
    {
        Value += amount;
    }

    public ResourceType GetResourceType()
    {
        return ResourceType.Wood;
    }

    public int GetValueCap()
    {
        return Player.StockpileMaximum.Value;
    }

    public InlineIconType GetInlineIconType()
    {
        return InlineIconType.Wood;
    }
}
