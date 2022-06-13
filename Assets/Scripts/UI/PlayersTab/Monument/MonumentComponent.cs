
using UnityEngine;
public class MonumentComponent
{
    public bool IsComplete { get; private set; } = false;
    public string Name { get; private set; }
    public MonumentComponentType MonumentComponentType { get; private set; }

    private MonumentComponentBlueprint _monumentComponentBlueprint;
    private float _remainingLabourTime;

    public MonumentComponent(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        _monumentComponentBlueprint = monumentComponentBlueprint;
        _remainingLabourTime = _monumentComponentBlueprint.LabourTime;

        Name = _monumentComponentBlueprint.Name;

        MonumentComponentType = _monumentComponentBlueprint.MonumentComponentType;
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
            UpdateRemainingLabourTime(-_monumentComponentBlueprint.LabourTime);
            SetCompleteStatus(true);
        }
    }

    public void ResetRemainingLabourTime()
    {
        _remainingLabourTime = _monumentComponentBlueprint.LabourTime;
    }

    public void UpdateRemainingLabourTime(float value)
    {
        Debug.Log($"_remainingLabourTime before {_remainingLabourTime}");
        _remainingLabourTime = _remainingLabourTime + value;
        Debug.Log($"_remainingLabourTime after {_remainingLabourTime}");

        if (_remainingLabourTime <= 0)
        {
            _remainingLabourTime = 0;
            SetCompleteStatus(true);
        }
    }
}

