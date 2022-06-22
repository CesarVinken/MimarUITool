
using UnityEngine;
public class MonumentComponent
{
    public bool IsComplete { get; private set; } = false;
    public string Name { get; private set; }
    public MonumentComponentType MonumentComponentType { get; private set; }
    public MonumentComponentBlueprint MonumentComponentBlueprint { get; private set; }

    private float _remainingLabourTime;

    public MonumentComponent(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        MonumentComponentBlueprint = monumentComponentBlueprint;
        _remainingLabourTime = MonumentComponentBlueprint.LabourTime;

        Name = MonumentComponentBlueprint.Name;

        MonumentComponentType = MonumentComponentBlueprint.MonumentComponentType;
    }

    private void SetCompleteStatus(bool isComplete)
    {
        IsComplete = isComplete;
    }

    public void SetMonumentComponentCompletion(bool isComplete)
    {
        if (IsComplete == isComplete) return;

        if (!isComplete)
        {
            ResetRemainingLabourTime();
            SetCompleteStatus(false);
        }
        else
        {
            UpdateRemainingLabourTime(-MonumentComponentBlueprint.LabourTime);
            SetCompleteStatus(true);
        }
    }

    public void ResetRemainingLabourTime()
    {
        _remainingLabourTime = MonumentComponentBlueprint.LabourTime;
    }

    public void UpdateRemainingLabourTime(float value)
    {
        _remainingLabourTime = _remainingLabourTime + value;

        if (_remainingLabourTime <= 0)
        {
            _remainingLabourTime = 0;
            SetCompleteStatus(true);
        }
    }
}

