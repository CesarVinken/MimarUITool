using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPriorityTile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Image _background;

    private PriorityNumber _priorityNumber;

    private void Awake()
    {
        if (_textField == null)
        {
            Debug.LogError($"Could not find text field on {gameObject.name}");
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

        _textField.text = $"{rankingNumber}. {player.Name} ({player.Reputation})";
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
