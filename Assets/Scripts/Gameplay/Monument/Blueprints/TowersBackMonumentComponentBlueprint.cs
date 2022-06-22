using System.Collections.Generic;

public class TowersBackMonumentComponentBlueprint : MonumentComponentBlueprint
{
    public override string Name { get { return _name; } }
    public override int LabourTime { get { return _labourTime; } }
    public override int ReputationGain { get { return _reputationGain; } }
    public override List<IResource> ResourceCosts { get { return _resourceCosts; } }
    public override MonumentComponentType MonumentComponentType { get { return _monumentComponentType; } }
    public override List<MonumentComponentType> Dependencies { get { return _dependencies; } }

    private string _name;
    private int _labourTime;
    private int _reputationGain;
    private List<IResource> _resourceCosts = new List<IResource>();
    private MonumentComponentType _monumentComponentType;
    private List<MonumentComponentType> _dependencies = new List<MonumentComponentType>();

    public static TowersBackMonumentComponentBlueprint Get()
    {
        MonumentComponentBlueprint blueprint = new TowersBackMonumentComponentBlueprint()
            .WithMonumentComponentType(MonumentComponentType.TowersBack)
            .WithLabourTime(20)
            .WithReputationGain(4)
            .WithMaterialCost(new Wood(10), new Granite(20))
            .WithName("Back towers");

        return blueprint as TowersBackMonumentComponentBlueprint;
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

    public override MonumentComponentBlueprint WithReputationGain(int reputationGain)
    {
        _reputationGain = reputationGain;
        return this;
    }

    public override MonumentComponentBlueprint WithMaterialCost(params IResource[] resources)
    {
        _resourceCosts.Clear();

        for (int i = 0; i < resources.Length; i++)
        {
            _resourceCosts.Add(resources[i]);
        }
        return this;
    }

    public override MonumentComponentBlueprint WithMonumentComponentType(MonumentComponentType monumentComponentType)
    {
        _monumentComponentType = monumentComponentType;
        return this;
    }

    public override void AddDependencies()
    {
        _dependencies.Add(MonumentComponentType.OuterWalls);
    }
}
