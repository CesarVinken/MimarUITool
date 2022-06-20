public class Marble : IResource
{
    public Player Player { get; private set; }
    public int Amount { get; private set; }


    public Marble(int amount)
    {
        Amount = amount;
    }

    public Marble(int amount, Player player)
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
        return ResourceType.Marble;
    }

    public int GetAmountCap()
    {
        return Player.StockpileMaximum.Amount;
    }
}
