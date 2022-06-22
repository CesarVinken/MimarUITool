using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtility : MonoBehaviour
{
    public static PriorityNumber IntToPlayerPriority(int priorityInt)
    {
        if (priorityInt == 1)
        {
            return PriorityNumber.Priority1;
        }
        if (priorityInt == 2)
        {
            return PriorityNumber.Priority2;
        }
        if (priorityInt == 3)
        {
            return PriorityNumber.Priority3;
        }
        Debug.LogError($"Cannot convert unknown priority int {priorityInt}");
        return PriorityNumber.Priority1;
    }

    public static int PlayerPriorityToInt(PriorityNumber priorityNumber)
    {
        switch (priorityNumber)
        {
            case PriorityNumber.Priority1:
                return 1;
            case PriorityNumber.Priority2:
                return 2;
            case PriorityNumber.Priority3:
                return 3;
            default:
                Debug.LogError($"Cannot convert unknown priority {priorityNumber}");
                return -1;
        }
    }

    public static bool CanAffordCost(List<IResource> resourceCosts, Dictionary<ResourceType, IResource> playerResources)
    {
        for (int i = 0; i < resourceCosts.Count; i++)
        {
            ResourceType resourceType = resourceCosts[i].GetResourceType();

            if (playerResources[resourceType].Amount < resourceCosts[i].Amount)
            {
                return false;
            }
        }

        return true;
    }
}
