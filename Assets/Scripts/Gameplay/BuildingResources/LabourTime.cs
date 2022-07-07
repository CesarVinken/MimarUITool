using UnityEngine;

public class LabourTime : IResource
{
    public Player Player { get; private set; }
    public int Value { get; private set; }

    public LabourTime(int amount)
    {
        Value = amount;
    }
    public LabourTime(int amount, Player player)
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
        return ResourceType.LabourTime;
    }

    public int GetValueCap()
    {
        return 999;
    }

    public InlineIconType GetInlineIconType()
    {
        return InlineIconType.LabourTime;
    }
}
