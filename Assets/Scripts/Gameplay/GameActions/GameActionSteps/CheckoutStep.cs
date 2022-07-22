using System;
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
        GameActionCheckSum gameActionCheckSum = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum;
        IGameAction gameAction = gameActionCheckSum.GameAction;
        string playerName = gameActionCheckSum.Player.Name;
        string costsString = WriteCosts(gameActionCheckSum);

        //if (previousStep is PickGameActionStep)
        //{
        //    return $"Perform a {gameActionCheckSum.GameAction.GetName()} action for {playerName}";
        //}
        if (gameAction is TravelGameAction)
        {
            return $"{playerName} will travel to {gameActionCheckSum.Location.Name}\n" +
                $"{costsString}";
        }
        else if (gameAction is MakeSacrificeGameAction)
        {
            return $"{playerName} will make a sacrifice!\n" +
                   $"{costsString}";
        }
        else if (gameAction is WorkerGameAction)
        {
            WorkerGameAction workerGameAction = gameActionCheckSum.GameAction as WorkerGameAction;

            if (workerGameAction == null)
            {
                Debug.LogError($"Could not parse the GameAction {gameActionCheckSum.GameAction.GetGameActionType()} as a WorkerGameAction");
            }

            int contractDuration = workerGameAction.GetContractLength();
            IWorker worker = workerGameAction.GetWorker();
            string wagesString = WriteWages(TempConfiguration.WorkerWages);

            switch (workerGameAction.GetWorkerActionType())
            {
                case WorkerActionType.ExtendContract:
                    int existingContractDuration = worker.ServiceLength;
                    return $"{playerName} will extend the contract of a worker by {contractDuration} turns to a total of {contractDuration + existingContractDuration} turns.\n" +
                            $"{costsString}" +
                            $"Per turn {playerName} will pay {wagesString} for upkeep";
                case WorkerActionType.Hire:
                    return $"{playerName} will hire a worker for {contractDuration} turns.\n" +
                            $"{costsString}" +
                            $"Per turn {playerName} will pay {wagesString} for upkeep";
                case WorkerActionType.Bribe:
                    Player targetPlayer = PlayerManager.Instance.Players[worker.Employer];
                    return $"{playerName} will bribe {PlayerUtility.GetPossessivePlayerString(targetPlayer)} worker at {gameActionCheckSum.Location.Name}\n" +
                            $"{costsString}" +
                            $"Per turn {playerName} will pay {wagesString} for upkeep\n";
                default:
                    new NotImplementedException($"No checkout was defined with the WorkerActionType {workerGameAction.GetWorkerActionType()}");
                    return "";
            }
        }
        else if(gameAction is UpgradeConstructionSiteGameAction)
        {
            PickConstructionSiteUpgradeStep pickConstructionSiteUpgradeStep = previousStep as PickConstructionSiteUpgradeStep;
            return $"Perform a {gameAction.GetName()} action for {gameActionCheckSum.Player.Name}\n" +
                $"{pickConstructionSiteUpgradeStep.GetEffectDescription()}\n" +
                $"{costsString}";
        }
        else
        {
            new NotImplementedException($"No checkout was defined for the GameAction {gameAction.GetName()}");
            return "";
        }
    }

    private string WriteCosts(GameActionCheckSum gameActionCheckSum)
    {
        List<IAccumulativePlayerStat> costs = gameActionCheckSum.GameAction.GetCosts();
        LocationType location = gameActionCheckSum.Location.LocationType;
        Player player = gameActionCheckSum.Player;
        string costsString = "";

        bool willTravel = GameActionUtility.WillTravel(location, player.Location.LocationType);
        int costParts = 0;
        if (costs.Count > 0)
        {
            costParts++;
        }

        if (willTravel)
        {
            costParts++;
        }
        if (costs.Count > 0)
        {
            string actionCostsString = GameActionUtility.GetActionCostsString(costParts, costs, player);
            costsString += actionCostsString;
        }

        if (willTravel)
        {
            string travelCostsString = GameActionUtility.GetTravellingCostsString(costParts, gameActionCheckSum.Location, player);
            costsString += travelCostsString;
        }

        if (costParts > 1)
        {
            string totalCostsString = GameActionUtility.GetTotalCostsString(costs, location, player);
            costsString += totalCostsString;
        }

        return costsString;
    }

    private string WriteWages(List<IAccumulativePlayerStat> wages)
    {
        string wagesString = "";
        for (int i = 0; i < wages.Count; i++)
        {
            int cost = wages[i].Value;
            wagesString += $"{Math.Abs(cost)} {wages[i].InlineIcon} ";
        }
        return wagesString;
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }
}
