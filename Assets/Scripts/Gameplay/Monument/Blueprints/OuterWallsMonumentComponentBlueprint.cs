public class OuterWallsMonumentComponentBlueprint : MonumentComponentBlueprint
{
    public override int LabourTime { get { return _labourTime; } }
    public override string Name { get { return _name; } }
    public override MonumentComponentType MonumentComponentType { get { return _monumentComponentType; } }

    private int _labourTime;
    private string _name;
    private MonumentComponentType _monumentComponentType;

    public static OuterWallsMonumentComponentBlueprint Get()
    {
        MonumentComponentBlueprint blueprint = new OuterWallsMonumentComponentBlueprint()
            .WithMonumentComponentType(MonumentComponentType.OuterWalls)
            .WithLabourTime(3)
            .WithName("Outer walls");

        return blueprint as OuterWallsMonumentComponentBlueprint;
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
