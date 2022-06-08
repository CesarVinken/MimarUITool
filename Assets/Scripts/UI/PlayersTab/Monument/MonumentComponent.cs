using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentComponent
{
    public bool Complete { get; private set; } = false;

    private MonumentComponentBlueprint _monumentComponentBlueprint;
    private int _remainingLabourTime;

    public MonumentComponent(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        _monumentComponentBlueprint = monumentComponentBlueprint;
        _remainingLabourTime = _monumentComponentBlueprint.LabourTime;
    }

    public void SetComplete(bool isComplete)
    {
        Complete = isComplete;
    }
}

