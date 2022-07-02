using System.Collections.Generic;
using UnityEngine;

public class GameActionUtility
{
    public static int CalculateStepNumber()
    {
        if (UIToolGameActionHandler.CurrentUIGameToolAction == null)
        {
            Debug.LogError($"Tried to calculate step number, but the Current UI Game Too Action is null. That should not happen");
        }

        List<IUIToolGameActionStep> steps = UIToolGameActionHandler.CurrentUIGameToolAction.GetSteps();

        int numberOfLastStep = steps.Count == 0 ? 0 : steps[steps.Count - 1].StepNumber;
        return numberOfLastStep + 1;
    }
}