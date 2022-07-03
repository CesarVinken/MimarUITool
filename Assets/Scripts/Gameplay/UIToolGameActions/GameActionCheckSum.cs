using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionCheckSum
{
    public Player Player { get; private set; }

    public void WithPlayer(Player player)
    {
        Player = player;
    }
}
