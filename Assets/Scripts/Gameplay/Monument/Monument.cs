using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monument
{
    private PlayerNumber _playerNumber;
    private List<MonumentComponent> _monumentComponents = new List<MonumentComponent>();

    public static List<MonumentComponentBlueprint> GetDefaultMonumentBlueprints()
    {
        List<MonumentComponentBlueprint> defaultMonumentComponentBlueprints = new List<MonumentComponentBlueprint>();

        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.Arches));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.Dome));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.FloorFirst));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.FloorSecond));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.FloorThird));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.GroundPlane));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.OuterWalls));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.TowersBack));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.TowersFront));
        defaultMonumentComponentBlueprints.Add(MonumentComponentBlueprint.Get(MonumentComponentType.TowersMiddle));

        return defaultMonumentComponentBlueprints;
    }

    public Monument(PlayerNumber playerNumber)
    {
        _playerNumber = playerNumber;

        InitialiseMonumentComponents();
    }

    // The new, initialised monument contains all the monument component but set to Complete = false;
    private void InitialiseMonumentComponents()
    {
        List<MonumentComponentBlueprint> defaultMonumentBlueprints = GetDefaultMonumentBlueprints();

        for (int i = 0; i < defaultMonumentBlueprints.Count; i++)
        {
            _monumentComponents.Add(new MonumentComponent(MonumentComponentBlueprint.Get(defaultMonumentBlueprints[i].MonumentComponentType)));
        }
    }

    public List<MonumentComponent> GetMonumentComponents()
    {
        return _monumentComponents;
    }

    public MonumentComponent GetMonumentComponentByType(MonumentComponentType monumentComponentType)
    {
        MonumentComponent monumentComponent = _monumentComponents.FirstOrDefault(m => m.MonumentComponentType == monumentComponentType);

        if(monumentComponent == null)
        {
            Debug.LogError($"Could not find a monument component type of type {monumentComponentType} among the monument component types in the monument for player {_playerNumber}");
        }

        return monumentComponent;
    }

    public void SetMonumentComponentCompletion(MonumentComponentType monumentComponentType, bool isComplete)
    {
        MonumentComponent monumentComponent = GetMonumentComponentByType(monumentComponentType);
        SetMonumentComponentCompletion(monumentComponent, isComplete);
    }

    public void SetMonumentComponentCompletion(MonumentComponent monumentComponent, bool isComplete)
    {
        monumentComponent.SetMonumentComponentCompletion(isComplete);
    }
}
