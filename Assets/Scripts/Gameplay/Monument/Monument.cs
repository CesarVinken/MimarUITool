using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monument
{
    private PlayerNumber _playerNumber;
    private List<MonumentComponent> _monumentComponents = new List<MonumentComponent>();

    public static List<MonumentComponentBlueprint> DefaultMonumentBlueprints = new List<MonumentComponentBlueprint>()
    {
        MonumentComponentBlueprint.Get(MonumentComponentType.Arches),
        MonumentComponentBlueprint.Get(MonumentComponentType.Dome),
        MonumentComponentBlueprint.Get(MonumentComponentType.FloorFirst),
        MonumentComponentBlueprint.Get(MonumentComponentType.FloorSecond),
        MonumentComponentBlueprint.Get(MonumentComponentType.FloorThird),
        MonumentComponentBlueprint.Get(MonumentComponentType.GroundPlane),
        MonumentComponentBlueprint.Get(MonumentComponentType.OuterWalls),
        MonumentComponentBlueprint.Get(MonumentComponentType.TowersBack),
        MonumentComponentBlueprint.Get(MonumentComponentType.TowersFront),
        MonumentComponentBlueprint.Get(MonumentComponentType.TowersMiddle)
    };

    public static void InitialiseDefaultMonumentBlueprints()
    {
        for (int i = 0; i < DefaultMonumentBlueprints.Count; i++)
        {
            DefaultMonumentBlueprints[i].AddDependencies();
        }
    }

    public Monument(PlayerNumber playerNumber)
    {
        _playerNumber = playerNumber;

        InitialiseMonumentComponents();
    }

    // The new, initialised monument contains all the monument component but set to Complete = false;
    private void InitialiseMonumentComponents()
    {
        Debug.LogWarning($"InitialiseMonumentComponents for {_playerNumber}");
        List<MonumentComponentBlueprint> defaultMonumentBlueprints = DefaultMonumentBlueprints;

        for (int i = 0; i < defaultMonumentBlueprints.Count; i++)
        {
            _monumentComponents.Add(new MonumentComponent(defaultMonumentBlueprints[i], _playerNumber));
        }

        MonumentComponent groundPlane = _monumentComponents.FirstOrDefault(m => m.MonumentComponentType == MonumentComponentType.GroundPlane);
        
        if(groundPlane == null)
        {
            Debug.LogError($"Could not find ground plane on monument");
        }

        groundPlane.UpdateMonumentComponentState(MonumentComponentState.Complete);

        // connect components to their dependencies
        for (int j = 0; j < _monumentComponents.Count; j++)
        {
            _monumentComponents[j].InitialiseDependencies(_monumentComponents);
        }

        for (int k = 0; k < _monumentComponents.Count; k++)
        {

            //Debug.Log($"Here {_monumentComponents[k].Name} has {_monumentComponents[k].Dependencies.Count} dependencies");
            MonumentComponent monumentComponent = _monumentComponents[k];
            for (int l = 0; l < monumentComponent.Dependencies.Count; l++)
            {
                MonumentComponent neededComponent = monumentComponent.Dependencies[l];
                if (neededComponent.State == MonumentComponentState.Complete)
                {
                    //Debug.LogWarning($"complee???");
                    monumentComponent.UpdateMonumentComponentState(MonumentComponentState.Buildable);
                }
            }
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

    public void SetMonumentComponentState(MonumentComponentType monumentComponentType, MonumentComponentState newState)
    {
        MonumentComponent monumentComponent = GetMonumentComponentByType(monumentComponentType);
        SetMonumentComponentState(monumentComponent, newState);
    }

    public void SetMonumentComponentState(MonumentComponent monumentComponent, MonumentComponentState newState)
    {
        monumentComponent.UpdateMonumentComponentState(newState);
    }
}
