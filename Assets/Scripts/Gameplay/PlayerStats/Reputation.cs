public class Reputation : IAccumulativePlayerStat
{
    public Player Player { get; private set; }
    public int Value { get; private set; }
    private int _amountCap = 999;
    public string InlineIcon { get; } = "<sprite=\"Icons\" index=4>";

    public Reputation(Player player)
    {
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

    public int GetValueCap()
    {
        return _amountCap;
    }
}
