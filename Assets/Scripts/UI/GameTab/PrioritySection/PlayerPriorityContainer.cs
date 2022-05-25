using System.Collections.Generic;
using System.Linq;
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
        Player player = PlayerManager.Instance.Players[PlayerNumber.Player1];
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
}
