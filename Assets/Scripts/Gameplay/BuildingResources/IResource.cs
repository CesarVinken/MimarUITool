public interface IResource
{
    int Amount { get; }

    void SetAmount(int newAmount);
    void AddAmount(int amount);
}
