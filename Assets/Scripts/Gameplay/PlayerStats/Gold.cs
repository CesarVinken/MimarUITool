
public class Gold : IAccumulativePlayerStat
{
    public Player Player { get; private set; }
    public int Value { get; private set; }
    public string InlineIcon { get; } = "<sprite=\"Icons\" index=0>";

    private int _amountCap = 999;

    public Gold(int amount)
    {
        Value = amount;
    }

    public Gold(Player player)
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
