
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
    public Dictionary<ResourceType, IResource> Resources { get; private set; } = new Dictionary<ResourceType, IResource>();

    public Monument Monument { get; private set; }

    public List<IWorker> HiredWorkers { get; private set; }

    public Player(PlayerNumber playerNumber, string playerName)
    {
        PlayerNumber = playerNumber;
        HiredWorkers = new List<IWorker>();
        Gold = 0;
        Reputation = 15;
        StockpileMaximum = 80;
        Name = playerName;
        Monument = new Monument(playerNumber);

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
        Resources.Add(ResourceType.Wood, new Wood(0));
        Resources.Add(ResourceType.Marble, new Marble(0));
        Resources.Add(ResourceType.Granite, new Granite(0));
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
        if (amount > StockpileMaximum)
        {
            Resources[resourceType].SetAmount(StockpileMaximum);
            return;
        }

        Resources[resourceType].SetAmount(amount);
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if(Resources[resourceType].Amount + amount > StockpileMaximum)
        {
            Resources[resourceType].SetAmount(StockpileMaximum);
            return;
        }

        Resources[resourceType].AddAmount(amount);

    }

    public void SetStockpileMaximum(int maximum)
    {
        StockpileMaximum = maximum;
    }

    public bool HandleMonumentComponentCompletion(MonumentComponentBlueprint monumentComponentBlueprint)
    {
        MonumentComponent monumentComponent = Monument.GetMonumentComponentByType(monumentComponentBlueprint.MonumentComponentType);
        Monument.SetMonumentComponentCompletion(monumentComponentBlueprint.MonumentComponentType, !monumentComponent.Complete);
        // update monument visuals
        // update workers
        // update component remaining labour time
        return monumentComponent.Complete;

    }
}
