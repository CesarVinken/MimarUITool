using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccumulativePlayerStatElementUIContainer : PlayerStatElementUIContainer
{
    [SerializeField] private TextMeshProUGUI _incomeProjectionLabel;

    public void Initialise(PlayerUIContent playerUIContentContainer, IAccumulativePlayerStat playerStatType)
    {
        _playerUIContentContainer = playerUIContentContainer;
        _playerStat = playerStatType;
    }

    public void UpdateUI(Player player, IAccumulativePlayerStat playerStat)
    {
        _player = player;
        _playerStat = playerStat;

        SetCurrentAmountDisplay(playerStat.Value);
        if (playerStat is IResource || playerStat is Gold)
        {
            _playerUIContentContainer.CalculateIncomeProjectionLabel(this, playerStat);
        }
    }

    public void SetIncomeProjectionLabel(string labelText)
    {
        _incomeProjectionLabel.text = labelText;
    }
}