using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();

    public CheckoutStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IGameActionElement> Initialise()
    {
        List<IGameActionStep> steps = GameActionStepHandler.CurrentGameActionSequence.GetSteps();
        IGameActionStep previousStep = null;

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

        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        string contentText = WriteCheckout(previousStep);
        IGameActionElement mainContentLabelElement = GameActionElementInitialiser.InitialiseMainContentLabel(this, contentText);
        _elements.Add(mainContentLabelElement);

        return _elements;
    }

    private string WriteCheckout(IGameActionStep previousStep)
    {
        if(previousStep is GameActionPickStep)
        {
            return $"Perform a {GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.GameAction.GetName()} action for {GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player.Name}";
        }
        else
        {
            Debug.LogError($"The action step type {previousStep.GetType()}");
            return "";
        }
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }
}
