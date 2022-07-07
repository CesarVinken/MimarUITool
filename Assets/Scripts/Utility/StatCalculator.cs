using UnityEngine;

public class StatCalculator : MonoBehaviour
{
    public static int CalculateGoldIncome(Player player)
    {
        int reputation = player.Reputation.Value;

        int baseIncome = 0;
        if (reputation <= -1)
            baseIncome = 17;
        else if (reputation <= 4)
            baseIncome = 18;
        else if (reputation <= 9)
            baseIncome = 19;
        else if (reputation <= 14)
            baseIncome = 20;
        else if (reputation <= 19)
            baseIncome = 21;
        else if (reputation <= 24)
            baseIncome = 22;
        else if (reputation <= 29)
            baseIncome = 23;
        else if (reputation <= 36)
            baseIncome = 24;
        else if (reputation <= 42)
            baseIncome = 25;
        else if (reputation <= 50)
            baseIncome = 26;
        else if (reputation <= 59)
            baseIncome = 27;
        else baseIncome = 28;

        // subtract wages
        int wages = 0;
        int workerWage = 2; // TODO: Move workerWage to list of configurable parameters
        for (int i = 0; i < player.HiredWorkers.Count; i++)
        {
            wages = wages + workerWage;
        }

        return baseIncome - wages;
    }

    public static int CalculateResourceIncome(ResourceType resourceType, Player player)
    {
        int resourceIncome = 0;
        for (int i = 0; i < player.HiredWorkers.Count; i++)
        {
            IWorker worker = player.HiredWorkers[i];
            LocationType locationType = worker.Location.LocationType;

            ILabourPoolLocation labourPoolLocation = LocationManager.Instance.GetLabourPoolLocation(locationType);

            if (labourPoolLocation.ResourceType == resourceType)
            {
                resourceIncome += 10; // Later the amount of collected resources will depend on modifiers
            }
        }

        return resourceIncome;
    }
}
