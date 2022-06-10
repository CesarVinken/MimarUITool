using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentComponent
{
    public bool Complete { get; private set; } = false;
    public string Name { get; private set; }
    public MonumentComponentType MonumentComponentType { get; private set; }

    private MonumentComponentBlueprint _monumentComponentBlueprint;
    private int _remainingLabourTime;

    public MonumentComponent(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        _monumentComponentBlueprint = monumentComponentBlueprint;
        _remainingLabourTime = _monumentComponentBlueprint.LabourTime;

        Name = _monumentComponentBlueprint.Name;

        MonumentComponentType = _monumentComponentBlueprint.MonumentComponentType;
    }

    public void SetComplete(bool isComplete)
    {
        Complete = isComplete;
    }
}

