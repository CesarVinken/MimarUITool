using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public Dictionary<PlayerNumber, Player> Players = new Dictionary<PlayerNumber, Player>();
    public List<Player> PlayersByPriority = new List<Player>();

    private void Awake()
    {
        Instance = this;

        Players.Add(PlayerNumber.Player1, new Player(PlayerNumber.Player1, "Cesar"));
        Players.Add(PlayerNumber.Player2, new Player(PlayerNumber.Player2, "Yigit"));
        Players.Add(PlayerNumber.Player3, new Player(PlayerNumber.Player3, "Ayhan"));

        InitialisePlayerPriority();
    }

    private void Start()
    {
    }

    public UIPlayerData GetPlayerData(PlayerNumber playerNumber)
    {
        Player player = Players[playerNumber];

        UIPlayerData playerData = new UIPlayerData()
            .WithPlayer(player);
            //.WithGold(player.Gold)
            //.WithReputation(player.Reputation)
            //.WithResources(player.Resources)
            //.WithStockpileMaximum(player.StockpileMaximum);

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
            if (newPriorityList[n].Reputation > updatedPlayer.Reputation)
            {
                priorityPosition++;
            }
            else if(newPriorityList[n].Reputation == updatedPlayer.Reputation &&
                oldReputation > newPriorityList[n].Reputation)
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

    public void PayIncomes()
    {
        foreach (KeyValuePair<PlayerNumber, Player> item in PlayerManager.Instance.Players)
        {
            Player player = item.Value;

            // TODO: If we do not have enough gold, force fire workers
            player.SetGold(player.Gold + StatCalculator.CalculateGoldIncome(player));

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
}