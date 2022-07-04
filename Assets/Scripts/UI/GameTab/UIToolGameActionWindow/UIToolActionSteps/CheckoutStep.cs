using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();

    public CheckoutStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        List<IUIToolGameActionStep> steps = UIToolGameActionHandler.CurrentUIGameToolAction.GetSteps();
        IUIToolGameActionStep previousStep = null;

        if(steps.Count < 2)
        {
            Debug.LogError($"There must be at least two steps in this action");
        }

        for (int i = 1; i < steps.Count; i++)
        {
            if(steps[i] is CheckoutStep)
            {
                previousStep = steps[i - 1]; 
            }
        }

        if(previousStep == null)
        {
            Debug.LogError($"Could not find a step before the checkout step");
        }

        IUIToolGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        string contentText = WriteCheckout(previousStep);
        IUIToolGameActionElement mainContentLabelElement = GameActionElementInitialiser.InitialiseMainContentLabel(this, contentText);
        _elements.Add(mainContentLabelElement);

        return _elements;
    }

    private string WriteCheckout(IUIToolGameActionStep previousStep)
    {
        if(previousStep is ActionPickStep)
        {
            return $"Perform a {UIToolGameActionHandler.CurrentUIGameToolAction.GameActionCheckSum.ActionType.GetType()} action for {UIToolGameActionHandler.CurrentUIGameToolAction.GameActionCheckSum.Player.Name}";
        }
        else
        {
            Debug.LogError($"The action step type {previousStep.GetType()}");
            return "";
        }
    }

    public void NextStep()
    {
        UIToolGameActionHandler.CurrentUIGameToolAction.NextStep();
    }
}
