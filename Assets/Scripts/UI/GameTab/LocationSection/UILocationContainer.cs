using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILocationContainer : MonoBehaviour, ILocationUIContainer
{
    protected LocationType _locationType;

    [SerializeField] protected Image[] _playerIconSlot;
    Dictionary<Player, Image> _usedPlayerIcons = new Dictionary<Player, Image>();

    public void SetLocationType(LocationType locationType)
    {
        _locationType = locationType;
    }

    
    public void AddPlayerToLocation(Player player)
    {
        //Debug.Log($"add {player.Name} to {_locationType} location");
        int numberOfUsedPlayerIcons = _usedPlayerIcons.Count;

        _usedPlayerIcons.Add(player, _playerIconSlot[numberOfUsedPlayerIcons]);
        _playerIconSlot[numberOfUsedPlayerIcons].sprite = player.Avatar;
        _playerIconSlot[numberOfUsedPlayerIcons].enabled = true;
    }

    public void RemovePlayerFromLocation(Player player)
    {
        //Debug.Log($"remove {player.Name} from {_locationType} location");
        _playerIconSlot[_usedPlayerIcons.Count - 1].sprite = null;
        _playerIconSlot[_usedPlayerIcons.Count - 1].enabled = false;

        if (_usedPlayerIcons.TryGetValue(player, out Image image))
        {
            _usedPlayerIcons.Remove(player);
        }

        Dictionary<Player, Image> _updatedUsedPlayerIcons = new Dictionary<Player, Image>();
        foreach (KeyValuePair<Player, Image> item in _usedPlayerIcons)
        {
            if (item.Key == player) continue;

            _updatedUsedPlayerIcons.Add(item.Key, item.Value);
            _playerIconSlot[_updatedUsedPlayerIcons.Count - 1].sprite = item.Key.Avatar;
            _playerIconSlot[_updatedUsedPlayerIcons.Count - 1].enabled = true;
        }
    }
}
