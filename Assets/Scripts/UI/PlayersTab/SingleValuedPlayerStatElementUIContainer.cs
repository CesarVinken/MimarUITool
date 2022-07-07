using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleValuedPlayerStatElementUIContainer : PlayerStatElementUIContainer
{
    public void Initialise(PlayerUIContent playerUIContentContainer, ISingleValuedPlayerStat playerStatType)
    {
        _playerUIContentContainer = playerUIContentContainer;
        _playerStat = playerStatType;
    }

    public void UpdateUI(Player player, ISingleValuedPlayerStat playerStat)
    {
        _player = player;
        _playerStat = playerStat;

        SetCurrentAmountDisplay(playerStat.Value);
    }
}
