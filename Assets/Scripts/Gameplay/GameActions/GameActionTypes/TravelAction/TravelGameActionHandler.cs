using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelGameActionHandler : MonoBehaviour, IGameActionHandler
{
    public void Handle(GameActionCheckSum gameActionCheckSum)
    {
        Player player = gameActionCheckSum.Player;
        ILocation targetLocation = gameActionCheckSum.Location;

        PlayerManager.Instance.GoToLocation(player, gameActionCheckSum.Location.LocationType);
        player.Gold.AddValue(-1);
    }
}