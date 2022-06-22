
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerNumber PlayerNumber { get; private set; }
    public PriorityNumber Priority { get; private set; }
    public string Name { get; private set; }
    public Color PlayerColour { get;  private set; }
    public Reputation Reputation { get; private set; }
    public Gold Gold { get; private set; }
    public StockpileMaximum StockpileMaximum { get; private set; }
    public Dictionary<ResourceType, IResource> Resources { get; private set; } = new Dictionary<ResourceType, IResource>();

    public Monument Monument { get; private set; }

    public List<IWorker> HiredWorkers { get; private set; }

    public Player(PlayerNumber playerNumber, string playerName)
    {
        PlayerNumber = playerNumber;
        Name = playerName;
        Reputation = new Reputation(this);
        Gold = new Gold(this);
        StockpileMaximum = new StockpileMaximum(this);

        HiredWorkers = new List<IWorker>();
        Gold.SetAmount(0);
        Reputation.SetAmount(15);
        StockpileMaximum.SetAmount(80);
        Monument = new Monument(playerNumber);


    }

    public void InitialisePlayer()
    {
        InitialiseResources();
        InitialisePlayerColour();

        InitialiseEventListeners();
    }

    private void InitialiseEventListeners()
    {
        if(GameFlowManager.Instance == null)
        {

            Debug.Log($"asdaksjaks");
        }
        GameFlowManager.Instance.MonumentComponentCompletionStateChangeEvent += OnMonumentComponentStateChange;
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
        Resources.Add(ResourceType.Wood, new Wood(0, this));
        Resources.Add(ResourceType.Marble, new Marble(0, this));
        Resources.Add(ResourceType.Granite, new Granite(0, this));
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
        Reputation.SetAmount(reputation);
    }

    public void SetGold(int goldAmount)
    {
        Gold.SetAmount(goldAmount);
    }

    public void SetResource(ResourceType resourceType, int amount)
    {
        if (amount > StockpileMaximum.Amount)
        {
            Resources[resourceType].SetAmount(StockpileMaximum.Amount);
            return;
        }

        Resources[resourceType].SetAmount(amount);
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if(Resources[resourceType].Amount + amount > StockpileMaximum.Amount)
        {
            Resources[resourceType].SetAmount(StockpileMaximum.Amount);
            return;
        }

        Resources[resourceType].AddAmount(amount);
    }

    public void SetStockpileMaximum(int maximum)
    {
        StockpileMaximum.SetAmount(maximum);
    }

    public void OnMonumentComponentStateChange(object sender, MonumentComponentCompletionStateChangeEvent e)
    {
        if (e.AffectedPlayer != PlayerNumber) return;

        if (e.State != MonumentComponentState.Complete) return;

        int gainedReputation = e.AffectedComponent.MonumentComponentBlueprint.ReputationGain;
        int oldReputation = Reputation.Amount;

        int newReputation = oldReputation + gainedReputation;

        SetReputation(newReputation);

        UpdatePlayerStatUIContent();
    }

    private void UpdatePlayerStatUIContent()
    {
        PlayersTabContainer playersTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.PlayersTab) as PlayersTabContainer;
        UIPlayerData playerData = PlayerManager.Instance.GetPlayerData(PlayerNumber);
        playersTabContainer.UpdatePlayerStatUIContent(playerData);
    }
}
