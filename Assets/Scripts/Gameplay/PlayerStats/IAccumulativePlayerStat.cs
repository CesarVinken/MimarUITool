public interface IAccumulativePlayerStat : IPlayerStat
{
    void SetValue(int newAmount);
    void AddValue(int amount);
    int GetValueCap();
    string InlineIcon { get; }
}
