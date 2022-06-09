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

        defaultMonumentComponentBlueprints.Add(FirstFloorMonumentComponentBlueprint.GetBlueprint());


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
        _monumentComponents.Add(new MonumentComponent(FirstFloorMonumentComponentBlueprint.GetBlueprint()));
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
        monumentComponent.SetComplete(isComplete);
    }
}
