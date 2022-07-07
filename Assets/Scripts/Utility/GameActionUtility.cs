using System.Collections.Generic;
using UnityEngine;

public class GameActionUtility
{
    public static int CalculateStepNumber()
    {
        if (GameActionStepHandler.CurrentGameActionSequence == null)
        {
            Debug.LogError($"Tried to calculate step number, but the Current UI Game Too Action is null. That should not happen");
        }

        List<IGameActionStep> steps = GameActionStepHandler.CurrentGameActionSequence.GetSteps();

        int numberOfLastStep = steps.Count == 0 ? 0 : steps[steps.Count - 1].StepNumber;
        return numberOfLastStep + 1;
    }
}