public class Granite : IResource, IPlayerStat
{
    public Player Player { get; private set; }
    public int Amount { get; private set; }


    public Granite(int amount)
    {
        Amount = amount;
    }

    public Granite(int amount, Player player)
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
        return ResourceType.Granite;
    }

    public int GetAmountCap()
    {
        return Player.StockpileMaximum.Amount;
    }
}
