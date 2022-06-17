using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStat
{
    Player Player { get; }
    int Amount { get; }

    void SetAmount(int newAmount);
    void AddAmount(int amount);
    int GetAmountCap();
}
