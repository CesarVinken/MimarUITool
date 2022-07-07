using System.Collections.Generic;
using UnityEngine;

public class PlayerPriorityContainer : MonoBehaviour
{
    [SerializeField] private PlayerPriorityTile _playerPriority1;
    [SerializeField] private PlayerPriorityTile _playerPriority2;
    [SerializeField] private PlayerPriorityTile _playerPriority3;

    public List<PlayerPriorityTile> PlayerPriorityTiles = new List<PlayerPriorityTile>();

    private void Awake()
    {
        if (_playerPriority1 == null)
        {
            Debug.LogError($"Could not find _playerPriority1 on {gameObject.name}");
        }
        if (_playerPriority2 == null)
        {
            Debug.LogError($"Could not find _playerPriority2 on {gameObject.name}");
        }
        if (_playerPriority3 == null)
        {
            Debug.LogError($"Could not find _playerPriority3 on {gameObject.name}");
        }
    }

    public void Initialise()
    {
        _playerPriority1.Initialise(PriorityNumber.Priority1);
        _playerPriority2.Initialise(PriorityNumber.Priority2);
        _playerPriority3.Initialise(PriorityNumber.Priority3);

        PlayerPriorityTiles.Add(_playerPriority1);
        PlayerPriorityTiles.Add(_playerPriority2);
        PlayerPriorityTiles.Add(_playerPriority3);

        UpdatePlayerPriorityTiles();
    }


    public void UpdatePlayerPriorityTiles()
    {
        for (int i = 0; i < PlayerPriorityTiles.Count; i++)
        {
            PlayerPriorityTiles[i].UpdatePriorityTile(PlayerManager.Instance.PlayersByPriority[i]); 
        }
    }

    public void RefreshPlayerMovesUI()
    {
        if(PlayerPriorityTiles.Count == 0)
        {
            Debug.LogError($"The player priority list is empty but should contain all the players");
        }

        for (int i = 0; i < PlayerPriorityTiles.Count; i++)
        {
            PlayerPriorityTiles[i].SetPlayerTurnStatus(true);
        }
    }

    public void UpdatePlayerMoveUI(Player player, bool canMove)
    {
        for (int i = 0; i < PlayerPriorityTiles.Count; i++)
        {
            if(PlayerPriorityTiles[i].Player.PlayerNumber == player.PlayerNumber)
            {
                PlayerPriorityTiles[i].SetPlayerTurnStatus(canMove);
                return;
            }
        }
    }
}
