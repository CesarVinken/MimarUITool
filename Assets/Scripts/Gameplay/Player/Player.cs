
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerNumber PlayerNumber { get; private set; }
    public PriorityNumber Priority { get; private set; }
    public string Name { get; private set; }
    public Sprite Avatar { get; private set; }
    public Color PlayerColour { get;  private set; }
    public Reputation Reputation { get; private set; }
    public Gold Gold { get; private set; }
    public StockpileMaximum StockpileMaximum { get; private set; }
    public Dictionary<ResourceType, IResource> Resources { get; private set; } = new Dictionary<ResourceType, IResource>();
    public IPlayerLocation Location { get; private set; }

    public Monument Monument { get; private set; }
    public bool CanMove { get; private set; }

    public List<IWorker> HiredWorkers { get; private set; }

    public Player(PlayerNumber playerNumber, string playerName)
    {
        PlayerNumber = playerNumber;
        Name = playerName;
        Reputation = new Reputation(this);
        Gold = new Gold(this);
        StockpileMaximum = new StockpileMaximum(this);

        HiredWorkers = new List<IWorker>();
        Reputation.SetValue(15);
        Gold.SetValue(StatCalculator.CalculateGoldIncome(this)); // give the player an initial amount of gold, based on their start reputation
        StockpileMaximum.SetLevel(new StockpileUpgrade(UpgradeLevel.Level0));
        Monument = new Monument(playerNumber);
    }

    public void InitialisePlayer()
    {
        InitialiseResources();
        InitialisePlayerColour();
        InitialiseEventListeners();

        Monument.InitialiseMonumentComponents();
        InitialiseAvatars();
        InitialiseLocation();
    }

    private void InitialiseLocation()
    {
        IPlayerLocation location;
        ILocationUIContainer newLocationUIContainer;

        switch (PlayerNumber)
        {
            case PlayerNumber.Player1:
                location = LocationManager.Instance.GetPlayerLocation(LocationType.ConstructionSite1);
                SetLocation(location);

                newLocationUIContainer = NavigationManager.Instance.GetLocationUIContainer(location.LocationType);
                newLocationUIContainer.AddPlayerToLocation(this);
                break;
            case PlayerNumber.Player2:
                location = LocationManager.Instance.GetPlayerLocation(LocationType.ConstructionSite2);
                SetLocation(location);

                newLocationUIContainer = NavigationManager.Instance.GetLocationUIContainer(location.LocationType);
                newLocationUIContainer.AddPlayerToLocation(this); 
                break;
            case PlayerNumber.Player3:
                location = LocationManager.Instance.GetPlayerLocation(LocationType.ConstructionSite3);
                SetLocation(location);

                newLocationUIContainer = NavigationManager.Instance.GetLocationUIContainer(location.LocationType);
                newLocationUIContainer.AddPlayerToLocation(this);
                break;
            default:
                break;
        }
    }

    private void InitialiseAvatars()
    {
        Avatar = AssetManager.Instance.GetAvatar(Name);
    }

    private void InitialiseEventListeners()
    {
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
                new NotImplementedException($"No colour set up for player {PlayerNumber}");
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
        Reputation.SetValue(reputation);
    }

    public void SetGold(int goldAmount)
    {
        Gold.SetValue(goldAmount);
    }

    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
    }

    public void SetLocation(IPlayerLocation location)
    {
        Location = location;
    }

    public void SetResource(ResourceType resourceType, int amount)
    {
        if (amount > StockpileMaximum.Value)
        {
            Resources[resourceType].SetValue(StockpileMaximum.Value);
            return;
        }

        Resources[resourceType].SetValue(amount);
    }

    public void AddResource(ResourceType resourceType, int amount)
    {
        if(Resources[resourceType].Value + amount > StockpileMaximum.Value)
        {
            Resources[resourceType].SetValue(StockpileMaximum.Value);
            return;
        }
        if(amount < 0 && Resources[resourceType].Value + amount < 0)
        {
            Resources[resourceType].SetValue(0);
            return;
        }

        Resources[resourceType].AddValue(amount);
    }

    public void OnMonumentComponentStateChange(object sender, MonumentComponentCompletionStateChangeEvent e)
    {
        if (e.AffectedPlayer != PlayerNumber) return;

        if (e.State == MonumentComponentState.Complete)
        {
            int gainedReputation = e.AffectedComponent.MonumentComponentBlueprint.ReputationGain;
            int oldReputation = Reputation.Value;
            int newReputation = oldReputation + gainedReputation;

            SetReputation(newReputation);

            UpdatePlayerStatUIContent();
        }
        else if (e.State == MonumentComponentState.InProgress) // we subtracted material costs and now we need to display this as well
        {
            UpdatePlayerStatUIContent();
        }
    }

    private void UpdatePlayerStatUIContent()
    {
        PlayersTabContainer playersTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.PlayersTab) as PlayersTabContainer;
        UIPlayerData playerData = PlayerManager.Instance.GetPlayerData(PlayerNumber);
        playersTabContainer.UpdatePlayerStatUIContent(playerData);
    }
}
