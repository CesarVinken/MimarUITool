
// The version of the monument component with all its original values, such as the base value of its costs and labour time.
// When we actually build a component we base it on this
using System.Collections.Generic;
using UnityEngine;


public abstract class MonumentComponentBlueprint
{
    // TODO For each component, add list of Prerequisite blueprints: the components that should have been completed before this model is available
    public abstract int LabourTime { get; }
    public abstract string Name { get; }
    public abstract int ReputationGain { get; }
    public abstract MonumentComponentType MonumentComponentType { get; }
    public abstract List<IResource> ResourceCosts { get; }


    public abstract MonumentComponentBlueprint WithName(string name);
    public abstract MonumentComponentBlueprint WithLabourTime(int labourTime);
    public abstract MonumentComponentBlueprint WithReputationGain(int reputationGain);
    public abstract MonumentComponentBlueprint WithMaterialCost(params IResource[] resources);

    public abstract MonumentComponentBlueprint WithMonumentComponentType(MonumentComponentType monumentComponentType);

    public static MonumentComponentBlueprint Get(MonumentComponentType monumentComponentType)
    {
        switch (monumentComponentType)
        {
            case MonumentComponentType.FloorFirst:
                return FloorFirstMonumentComponentBlueprint.Get();
            case MonumentComponentType.FloorSecond:
                return FloorSecondMonumentComponentBlueprint.Get();
            case MonumentComponentType.FloorThird:
                return FloorThirdMonumentComponentBlueprint.Get();
            case MonumentComponentType.Arches:
                return ArchesMonumentComponentBlueprint.Get();
            case MonumentComponentType.Dome:
                return DomeMonumentComponentBlueprint.Get();
            case MonumentComponentType.GroundPlane:
                return GroundPlaneMonumentComponentBlueprint.Get();
            case MonumentComponentType.OuterWalls:
                return OuterWallsMonumentComponentBlueprint.Get();
            case MonumentComponentType.TowersFront:
                return TowersFrontMonumentComponentBlueprint.Get();
            case MonumentComponentType.TowersBack:
                return TowersBackMonumentComponentBlueprint.Get();
            case MonumentComponentType.TowersMiddle:
                return TowersMiddleMonumentComponentBlueprint.Get();
            default:
                Debug.LogError($"Could not find an implementation for the monument component type '{monumentComponentType}'");
                return null;
        }
    }
}
