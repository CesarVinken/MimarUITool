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
    }

    // The new, initialised monument contains all the monument component but set to Complete = false;
    public void InitialiseMonumentComponents()
    {
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

        // components that have no more requirements become buildable/unaffordable, those that still have dependencies stay locked
        UpdateDependencies();
    }

    public void UpdateDependencies()
    {
        Debug.LogWarning($"update dependencies");
        for (int i = 0; i < _monumentComponents.Count; i++)
        {

            MonumentComponent monumentComponent = _monumentComponents[i];
            for (int j = 0; j < monumentComponent.Dependencies.Count; j++)
            {
                MonumentComponent neededComponent = monumentComponent.Dependencies[j];
                //Debug.Log($"going over {monumentComponent.MonumentComponentType} with the state {monumentComponent.State}. It has a dependency {neededComponent.MonumentComponentType} with the state {neededComponent.State}");
                if (neededComponent.State == MonumentComponentState.Complete)
                {
                    if (monumentComponent.State != MonumentComponentState.Locked) continue;

                    Player player = PlayerManager.Instance.Players[_playerNumber];
                    bool canAffordCost = PlayerUtility.CanAffordCost(
                        monumentComponent.MonumentComponentBlueprint.ResourceCosts,
                        player.Resources
                        );
                    MonumentComponentState buildableState = canAffordCost ? MonumentComponentState.Buildable : MonumentComponentState.Unaffordable;
                    monumentComponent.UpdateMonumentComponentState(buildableState);
                }
                else // this would only happen if a already finished component is forced to be "unfinished". That would force other dependent components to be locked again too.
                {
                    if(monumentComponent.State != MonumentComponentState.Locked)
                    {
                        monumentComponent.UpdateMonumentComponentState(MonumentComponentState.Locked);
                    }
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
