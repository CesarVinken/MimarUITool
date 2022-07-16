using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        GameActionCheckSum gameActionCheckSum = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum;
        if (previousStep is PickGameActionStep)
        {
            return $"Perform a {gameActionCheckSum.GameAction.GetName()} action for {gameActionCheckSum.Player.Name}";
        }
        else if (previousStep is PickTravelLocationStep)
        {
            return $"{gameActionCheckSum.GameAction.GetName()} will travel to {gameActionCheckSum.Location.Name}\n" +
                $"The costs will be 1 {AssetManager.Instance.GetInlineIcon(InlineIconType.Gold)}";
        }
        else if(previousStep is PickConstructionSiteUpgradeStep)
        {
            PickConstructionSiteUpgradeStep pickConstructionSiteUpgradeStep = previousStep as PickConstructionSiteUpgradeStep;
            string costsString = WriteCosts(gameActionCheckSum);
            return $"Perform a {gameActionCheckSum.GameAction.GetName()} action for {gameActionCheckSum.Player.Name}\n" +
                $"{pickConstructionSiteUpgradeStep.GetEffectDescription()}\n" +
                $"The costs will be {costsString}";
        }
        else
        {
            new NotImplementedException($"No checkout was defined with the previous step {previousStep.GetType()}");
            return "";
        }
    }

    private string WriteCosts(GameActionCheckSum gameActionCheckSum)
    {
        string costsString = "";

        List<IAccumulativePlayerStat> costs = gameActionCheckSum.GameAction.GetCosts();

        int goldCosts = 0;
        bool willTravel = gameActionCheckSum.Player.Location.LocationType != gameActionCheckSum.Location.LocationType;

        if (willTravel)
        {
            goldCosts++;
        }

        for (int i = 0; i < costs.Count; i++)
        {
            int cost = costs[i].Value;
            if(costs[i] is Gold)
            {
                cost += goldCosts;
            }
            costsString += $"{costs[i].Value} {costs[i].InlineIcon} ";
        }
        if(goldCosts > 0 && costs.FirstOrDefault(c => c is Gold) == null)
        {
            costsString += $"{goldCosts} {AssetManager.Instance.GetInlineIcon(InlineIconType.Gold)} ";
        }

        return costsString;
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }
}
