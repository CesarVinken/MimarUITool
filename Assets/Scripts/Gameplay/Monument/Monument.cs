using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monument
{
    List<MonumentComponent> MonumentComponents = new List<MonumentComponent>();

    public static List<MonumentComponentBlueprint> GetDefaultMonumentBlueprints()
    {
        List<MonumentComponentBlueprint> defaultMonumentComponentBlueprints = new List<MonumentComponentBlueprint>();

        defaultMonumentComponentBlueprints.Add(FirstFloorMonumentComponentBlueprint.GetBlueprint());


        return defaultMonumentComponentBlueprints;
    }

    public Monument()
    {
        InitialiseMonumentComponents();
    }

    private void InitialiseMonumentComponents()
    {
        MonumentComponents.Add(new MonumentComponent(FirstFloorMonumentComponentBlueprint.GetBlueprint()));
    }
}
