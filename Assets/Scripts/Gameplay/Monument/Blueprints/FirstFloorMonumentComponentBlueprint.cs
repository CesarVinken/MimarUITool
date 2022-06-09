using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorMonumentComponentBlueprint : MonumentComponentBlueprint
{
    public override int LabourTime { get { return _labourTime; } }
    public override string Name { get { return _name; } }
    public override MonumentComponentType MonumentComponentType { get { return _monumentComponentType; } }

    private int _labourTime;
    private string _name;
    private MonumentComponentType _monumentComponentType;

    public static FirstFloorMonumentComponentBlueprint GetBlueprint()
    {
        MonumentComponentBlueprint blueprint = new FirstFloorMonumentComponentBlueprint()
            .WithMonumentComponentType(MonumentComponentType.FirstFloor)
            .WithLabourTime(3)
            .WithName("1st Floor");

        return blueprint as FirstFloorMonumentComponentBlueprint;
    }

    public override MonumentComponentBlueprint WithLabourTime(int labourTime)
    {
        _labourTime = labourTime;
        return this;
    }

    public override MonumentComponentBlueprint WithName(string name)
    {
        _name = name;
        return this;
    }

    public override MonumentComponentBlueprint WithMonumentComponentType(MonumentComponentType monumentComponentType)
    {
        _monumentComponentType = monumentComponentType;
        return this;
    }
}
