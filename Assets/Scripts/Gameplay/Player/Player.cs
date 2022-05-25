
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerNumber PlayerNumber { get; private set; }
    public PriorityNumber Priority { get; private set; }
    public string Name { get; private set; }
    public Color PlayerColour { get;  private set; }
    public int Reputation { get; private set; }
    public int Gold { get; private set; }
    public int StockpileMaximum { get; private set; }
    public Dictionary<ResourceType, int> Resources { get; private set; } = new Dictionary<ResourceType, int>();


    public List<IWorker> HiredWorkers { get; private set; }

    public Player(PlayerNumber playerNumber, string playerName)
    {
        PlayerNumber = playerNumber;
        HiredWorkers = new List<IWorker>();
        Gold = 0;
        Reputation = 15;
        StockpileMaximum = 8;
        Name = playerName;
        InitialiseResources();
        InitialisePlayerColour();
    }

    private void InitialisePlayerColour()
    {
        switch (PlayerNumber)
        {
            case PlayerNumber.Player1:
                PlayerColour = ColourUtility.GetColour(ColourType.Player1);
                break;
            case PlayerNumber.Player2:
                PlayerColour = ColourUtility.GetColour(ColourType.Player2);
                break;
            case PlayerNumber.Player3:
                PlayerColour = ColourUtility.GetColour(ColourType.Player3);
                break;
            default:
                Debug.LogError($"No colour set up for player {PlayerNumber}");
                break;
        }
    }

    private void InitialiseResources()
    {
        Resources.Add(ResourceType.Wood, 0);
        Resources.Add(ResourceType.Marble, 0);
        Resources.Add(ResourceType.Granite, 0);
    }

    public void AddWorker(IWorker worker)
    {
        HiredWorkers.Add(worker);
    }

    public void SetPriority(PriorityNumber priorityNumber)
    {
        Priority = priorityNumber;
    }

    public void RemoveWorker(IWorker worker)
    {
        HiredWorkers.Remove(worker);
    }

    public void SetReputation(int reputation)
    {
        Reputation = reputation;
    }

    public void SetGold(int gold)
    {
        Gold = gold;
    }

    public void SetResource(ResourceType resourceType, int amount)
    {
        Resources[resourceType] = amount;
    }

    public void SetStockpileMaximum(int maximum)
    {
        StockpileMaximum = maximum;
    }
}
