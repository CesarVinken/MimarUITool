using UnityEngine;

public class LabourTime : IResource
{
    public Player Player { get; private set; }
    public int Amount { get; private set; }

    public LabourTime(int amount)
    {
        Amount = amount;
    }
    public LabourTime(int amount, Player player)
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
        return ResourceType.LabourTime;
    }

    public int GetAmountCap()
    {
        return 999;
    }

    public InlineIconType GetInlineIconType()
    {
        return InlineIconType.LabourTime;
    }
}
