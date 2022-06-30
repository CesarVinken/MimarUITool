using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPriorityTile : MonoBehaviour
{
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private TextMeshProUGUI _playerNameTextField;
    [SerializeField] private TextMeshProUGUI _playerInfoTextField;
    [SerializeField] private Image _background;

    private PriorityNumber _priorityNumber;

    private void Awake()
    {
        if (_playerNameTextField == null)
        {
            Debug.LogError($"Could not find _playerNameTextField on {gameObject.name}");
        }
        if (_playerAvatar == null)
        {
            Debug.LogError($"Could not find _playerAvatar on {gameObject.name}");
        }
        if (_playerInfoTextField == null)
        {
            Debug.LogError($"Could not find _playerInfoTextField on {gameObject.name}");
        }
        if (_background == null)
        {
            Debug.LogError($"Could not find background on {gameObject.name}");
        }
    }

    public void Initialise(PriorityNumber priorityNumber)
    {
        _priorityNumber = priorityNumber;
    }

    public void UpdatePriorityTile(Player player)
    {
        SetAvatar(player);
        SetText(player);
        SetColour(player.PlayerNumber);
    }

    private void SetText(Player player)
    {
        int rankingNumber = 1;
        if (_priorityNumber == PriorityNumber.Priority2)
        {
            rankingNumber = 2;
        }
        else if (_priorityNumber == PriorityNumber.Priority3)
        {
            rankingNumber = 3;
        }

        //_playerNameTextField.text = $"{rankingNumber}. {player.Name}";
        _playerNameTextField.text = $"{rankingNumber}.";
        _playerInfoTextField.text = $"{AssetManager.Instance.GetInlineIcon(InlineIconType.Reputation)}: {player.Reputation.Amount} {AssetManager.Instance.GetInlineIcon(InlineIconType.Gold)}: {player.Gold.Amount}";
    }

    private void SetAvatar(Player player)
    {
        _playerAvatar.sprite = player.Avatar;
    }

    private void SetColour(PlayerNumber playerNumber)
    {
        if (PlayerManager.Instance.Players.TryGetValue(playerNumber, out Player player))
        {
            _background.color = player.PlayerColour;
        }
        else
        {
            _background.color = ColourUtility.GetColour(ColourType.Empty);
        }
    }
}
