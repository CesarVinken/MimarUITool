using UnityEngine;

public class TravelGameActionHandler : IGameActionHandler
{
    public void Handle(GameActionCheckSum gameActionCheckSum)
    {
        Player player = gameActionCheckSum.Player;
        ILocation targetLocation = gameActionCheckSum.Location;

        PlayerManager.Instance.GoToLocation(player, targetLocation.LocationType);
        player.SetGold(player.Gold.Value - 1);
    }
}