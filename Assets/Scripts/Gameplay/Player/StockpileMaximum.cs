public class StockpileMaximum : IPlayerStat
{
    public Player Player { get; private set; }
    public int Amount { get; private set; }
    private int _amountCap = 999;

    public StockpileMaximum(Player player)
    {
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

    public int GetAmountCap()
    {
        return _amountCap;
    }
}
