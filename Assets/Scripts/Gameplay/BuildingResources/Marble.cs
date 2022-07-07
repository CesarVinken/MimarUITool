public class Marble : IResource
{
    public Player Player { get; private set; }
    public int Value { get; private set; }


    public Marble(int amount)
    {
        Value = amount;
    }

    public Marble(int amount, Player player)
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
        return ResourceType.Marble;
    }

    public int GetValueCap()
    {
        return Player.StockpileMaximum.Value;
    }

    public InlineIconType GetInlineIconType()
    {
        return InlineIconType.Marble;
    }
}
