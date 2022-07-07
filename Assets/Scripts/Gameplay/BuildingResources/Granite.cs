public class Granite : IResource, IPlayerStat
{
    public Player Player { get; private set; }
    public int Value { get; private set; }


    public Granite(int amount)
    {
        Value = amount;
    }

    public Granite(int amount, Player player)
    {
        Value = amount;
        Player = player;
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
        return ResourceType.Granite;
    }

    public int GetValueCap()
    {
        return Player.StockpileMaximum.Value;
    }

    public InlineIconType GetInlineIconType()
    {
        return InlineIconType.Granite;
    }
}
