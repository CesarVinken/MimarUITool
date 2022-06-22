
using System.Collections.Generic;
using UnityEngine;



public class MonumentComponent
{
    public MonumentComponentState State { get; private set; } = MonumentComponentState.Locked;
    public string Name { get; private set; }
    public MonumentComponentType MonumentComponentType { get; private set; }
    public MonumentComponentBlueprint MonumentComponentBlueprint { get; private set; }
    public List<MonumentComponent> Dependencies { get; private set; }

    public float RemainingLabourTime { get; private set; }

    public MonumentComponent(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        MonumentComponentBlueprint = monumentComponentBlueprint;
        RemainingLabourTime = MonumentComponentBlueprint.LabourTime;

        Name = MonumentComponentBlueprint.Name;

        MonumentComponentType = MonumentComponentBlueprint.MonumentComponentType;
    }

    public void InitialiseDependencies(List<MonumentComponent> monumentComponents)
    {
        Dependencies = new List<MonumentComponent>();

        for (int i = 0; i < monumentComponents.Count; i++)
        {
            MonumentComponent otherMonumentComponent = monumentComponents[i];

            if (otherMonumentComponent == this) continue;

            for (int j = 0; j < MonumentComponentBlueprint.Dependencies.Count; j++)
            {
                MonumentComponentType dependencyType = MonumentComponentBlueprint.Dependencies[j];

                if (otherMonumentComponent.MonumentComponentType == dependencyType)
                {
                    Dependencies.Add(otherMonumentComponent);
                }
            }
        }
    }

    private void SetState(MonumentComponentState newState)
    {
        State = newState;
    }

    public void UpdateMonumentComponentState(MonumentComponentState newState)
    {
        if (State == newState) return;

        if(State == MonumentComponentState.Complete)
        {
            ResetRemainingLabourTime();
        }

        switch (newState)
        {
            case MonumentComponentState.Locked:
                break;
            case MonumentComponentState.Unaffordable:
                break;
            case MonumentComponentState.Buildable:
                break;
            case MonumentComponentState.InProgress:
                break;
            case MonumentComponentState.Complete:
                SetRemainingLabourTime(0);
                break;
            default:
                break;
        }
        SetState(newState);
    }

    public void ResetRemainingLabourTime()
    {
        RemainingLabourTime = MonumentComponentBlueprint.LabourTime;
    }

    public void SetRemainingLabourTime(float value)
    {
        RemainingLabourTime = value;

        if (RemainingLabourTime <= 0)
        {
            RemainingLabourTime = 0;
            SetState(MonumentComponentState.Complete);
        }
    }
}

