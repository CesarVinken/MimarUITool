using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Dictionary<PlayerNumber, Player> Players = new Dictionary<PlayerNumber, Player>();
    public List<Player> PlayersByPriority = new List<Player>();

    public void Setup()
    {
        Instance = this;

        Monument.InitialiseDefaultMonumentBlueprints();

        Players.Add(PlayerNumber.Player1, new Player(PlayerNumber.Player1, "Cesar"));
        Players.Add(PlayerNumber.Player2, new Player(PlayerNumber.Player2, "Yigit"));
        Players.Add(PlayerNumber.Player3, new Player(PlayerNumber.Player3, "Mustafa"));
    }

    public void InitialisePlayers()
    {
        foreach (KeyValuePair<PlayerNumber, Player> item in Players)
        {
            item.Value.InitialisePlayer();
        }
    }

    public UIPlayerData GetPlayerData(PlayerNumber playerNumber)
    {
        Player player = Players[playerNumber];

        UIPlayerData playerData = new UIPlayerData()
            .WithPlayer(player);
            
        return playerData;
    }

    public void InitialisePlayerPriority()
    {
        PlayersByPriority.Clear();

        List<Player> players = Players.Values.ToList();

        //randomly sort list
        for (int i = 0; i < players.Count; i++)
        {
            Player temp = players[i];
            int randomIndex = UnityEngine.Random.Range(i, players.Count);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
        }

        for (int j = 0; j < players.Count; j++)
        {
            players[j].SetPriority(PlayerUtility.IntToPlayerPriority(j + 1));
            PlayersByPriority.Add(players[j]);
        }
    }

    public void UpdatePlayerPriority(Player updatedPlayer, int oldReputation)
    {
        List<Player> newPriorityList = PlayersByPriority;
        newPriorityList.Remove(updatedPlayer);

        int priorityPosition = 1;
        for (int n = 0; n < newPriorityList.Count; n++)
        {
            if (newPriorityList[n].Reputation.Value > updatedPlayer.Reputation.Value)
            {
                priorityPosition++;
            }
            else if(newPriorityList[n].Reputation.Value == updatedPlayer.Reputation.Value &&
                oldReputation > newPriorityList[n].Reputation.Value)
            {
                priorityPosition++;
            }
        }

        newPriorityList.Insert(priorityPosition - 1, updatedPlayer);

        for (int k = 0; k < newPriorityList.Count; k++)
        {
            newPriorityList[k].SetPriority(PlayerUtility.IntToPlayerPriority(k + 1));
        }

        PlayersByPriority = newPriorityList;
    }

    public void RefreshPlayerMoves()
    {
        List<Player> players = Players.Values.ToList();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetCanMove(true);
        }

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.RefreshPlayerMovesUI();
    }

    public void UpdatePlayerMove(Player player, bool canMove)
    {
        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.UpdatePlayerMove(player, canMove);

        player.SetCanMove(canMove);
    }

    public void PayIncomes()
    {
        foreach (KeyValuePair<PlayerNumber, Player> item in PlayerManager.Instance.Players)
        {
            Player player = item.Value;

            // TODO: If we do not have enough gold, force fire workers
            player.SetGold(player.Gold.Value + StatCalculator.CalculateGoldIncome(player));
        }
    }

    public void DistractWorkerServiceLength()
    {
        foreach (KeyValuePair<PlayerNumber, Player> item in PlayerManager.Instance.Players)
        {
            Player player = item.Value;
            for (int i = player.HiredWorkers.Count - 1; i >= 0; i--)
            {
                IWorker worker = player.HiredWorkers[i];
                worker.UIWorkerTile.SubtractServiceLength(); // Maybe this should not be directed through the worker tile, but through some manager?
            }
        }
    }

    public void CollectResources()
    {
        foreach (KeyValuePair<PlayerNumber, Player> playerItem in PlayerManager.Instance.Players)
        {
            Player player = playerItem.Value;

            foreach (KeyValuePair<ResourceType, IResource> resourceItem in player.Resources)
            {
                int addedAmount = StatCalculator.CalculateResourceIncome(resourceItem.Key, player);

                player.AddResource(resourceItem.Key, addedAmount);
            }
        }
    }

    public void PerformBuildingTasks()
    {
        foreach (KeyValuePair<PlayerNumber, Player> playerItem in PlayerManager.Instance.Players)
        {
            Player player = playerItem.Value;

            for (int i = 0; i < player.HiredWorkers.Count; i++)
            {
                CityWorker worker = player.HiredWorkers[i] as CityWorker;

                if (worker == null) continue;
                if (worker.CurrentBuildingTask == null) continue;

                worker.Build();
            }
        }
    }

    public void GoToLocation(Player player, LocationType newLocation)
    {
        if (player.Location != null)
        {
            LocationType oldLocation = player.Location.LocationType;
            ILocationUIContainer oldLocationUIContainer = NavigationManager.Instance.GetLocationUIContainer(oldLocation);
            oldLocationUIContainer.RemovePlayerFromLocation(player);
        }

        IPlayerLocation location = LocationManager.Instance.GetPlayerLocation(newLocation);
        player.SetLocation(location);

        ILocationUIContainer newLocationUIContainer = NavigationManager.Instance.GetLocationUIContainer(newLocation);
        newLocationUIContainer.AddPlayerToLocation(player);
    }
}