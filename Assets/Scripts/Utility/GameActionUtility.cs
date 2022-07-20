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
    public static string GetCostsString(List<IAccumulativePlayerStat> costs, LocationType actionLocationType, Player actingPlayer)
    {
        string costsString = "";
        bool willTravel = actingPlayer.Location.LocationType != actionLocationType;
        int travelCost = -1;

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
}