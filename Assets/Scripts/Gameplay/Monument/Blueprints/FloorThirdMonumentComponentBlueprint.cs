public class FloorThirdMonumentComponentBlueprint : MonumentComponentBlueprint
{
    public override int LabourTime { get { return _labourTime; } }
    public override string Name { get { return _name; } }
    public override MonumentComponentType MonumentComponentType { get { return _monumentComponentType; } }

    private int _labourTime;
    private string _name;
    private MonumentComponentType _monumentComponentType;

    public static FloorThirdMonumentComponentBlueprint Get()
    {
        MonumentComponentBlueprint blueprint = new FloorThirdMonumentComponentBlueprint()
            .WithMonumentComponentType(MonumentComponentType.FloorThird)
            .WithLabourTime(3)
            .WithName("3rd Floor");

        return blueprint as FloorThirdMonumentComponentBlueprint;
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
