using System;
using System.Collections.Generic;
using System.Linq;
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

    public static bool WillTravel(LocationType actionLocationType, LocationType playerLocation)
    {
        return playerLocation != actionLocationType;
    }

    public static string GetTravellingCostsString(int costParts, ILocation destination, Player actingPlayer)
    {
        string travelCostsString = "";
        int normalisedTravellingCost = Math.Abs(TempConfiguration.TravellingGoldCost);

        if (costParts > 1 && actingPlayer.Gold.Value < normalisedTravellingCost)
        {
            travelCostsString += $"For travelling to {destination.Name} the player needs to pay <color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{normalisedTravellingCost}</color> {AssetManager.Instance.GetInlineIcon(InlineIconType.Gold)}\n";
        }
        else
        {
            travelCostsString += $"For travelling to {destination.Name} the player will pay {normalisedTravellingCost} {AssetManager.Instance.GetInlineIcon(InlineIconType.Gold)}\n";
        }

        return travelCostsString;
    }

    public static string GetActionCostsString(int costParts, List<IAccumulativePlayerStat> costs, Player actingPlayer)
    {
        string costsString = "The costs of the action are: ";

        for (int i = 0; i < costs.Count; i++)
        {
            int cost = costs[i].Value;

            IAccumulativePlayerStat playerStat = actingPlayer.GetPlayerStat(costs[i]);
            if (costParts > 1 && playerStat.Value < Math.Abs(cost))
            {
                costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(cost)}</color> {costs[i].InlineIcon} ";
            }
            else
            {
                costsString += $"{Math.Abs(cost)} {costs[i].InlineIcon} ";
            }
        }

        costsString += "\n";
        return costsString;
    }

    public static string GetTotalCostsTileString(List<IAccumulativePlayerStat> costs, LocationType actionLocationType, Player actingPlayer)
    {
        string costsString = "";
        bool willTravel = WillTravel(actionLocationType, actingPlayer.Location.LocationType);
        int travelCost = TempConfiguration.TravellingGoldCost;

        if (willTravel)
        {
            IAccumulativePlayerStat gold = costs.FirstOrDefault(c => c is Gold);
            if (gold == null)
            {
                Gold g = new Gold(travelCost);

                if (actingPlayer.Gold.Value < Math.Abs(g.Value))
                {
                    costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(g.Value)}</color> {g.InlineIcon} ";
                }
                else
                {
                    costsString += $"{Math.Abs(g.Value)} {g.InlineIcon} ";
                }
            }
        }

        for (int i = 0; i < costs.Count; i++)
        {
            int cost = costs[i].Value;
            if (willTravel && costs[i] is Gold)
            {
                cost += travelCost;
            }

            IAccumulativePlayerStat playerStat = actingPlayer.GetPlayerStat(costs[i]);
            if (playerStat.Value < Math.Abs(cost))
            {
                costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(cost)}</color> {costs[i].InlineIcon} ";
            }
            else
            {
                costsString += $"{Math.Abs(cost)} {costs[i].InlineIcon} ";
            }
        }

        return costsString;
    }

    public static string GetTotalCostsString(List<IAccumulativePlayerStat> costs, LocationType actionLocationType, Player actingPlayer)
    {
        string costsString = "The total costs are: ";
        bool willTravel = WillTravel(actionLocationType, actingPlayer.Location.LocationType);
        int travelCost = TempConfiguration.TravellingGoldCost;

        if (willTravel)
        {
            IAccumulativePlayerStat gold = costs.FirstOrDefault(c => c is Gold);
            if (gold == null)
            {
                Gold g = new Gold(travelCost);

                if (actingPlayer.Gold.Value < Math.Abs(g.Value))
                {
                    costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(g.Value)}</color> {g.InlineIcon} ";
                }
                else
                {
                    costsString += $"{Math.Abs(g.Value)} {g.InlineIcon} ";
                }    
            }
        }
     
        for (int i = 0; i < costs.Count; i++)
        {
            int cost = costs[i].Value;
            if (willTravel && costs[i] is Gold)
            {
                cost += travelCost;
            }

            IAccumulativePlayerStat playerStat = actingPlayer.GetPlayerStat(costs[i]);
            if (playerStat.Value < Math.Abs(cost))
            {
                costsString += $"<color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{Math.Abs(cost)}</color> {costs[i].InlineIcon} ";
            }
            else
            {
                costsString += $"{Math.Abs(cost)} {costs[i].InlineIcon} ";
            }
        }

        costsString += "\n";
        return costsString;
    }
}