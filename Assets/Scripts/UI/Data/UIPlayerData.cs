using System.Collections.Generic;
using UnityEngine;

public class UIPlayerData
{
    public Player Player { get; private set; }

    public UIPlayerData WithPlayer(Player player)
    {
        Player = player;
        return this;
    }
}
