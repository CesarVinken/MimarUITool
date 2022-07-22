
using System.Collections.Generic;
using UnityEngine;

public class MakeSacrificeGameActionHandler : IGameActionHandler
{
    private GameActionCheckSum _gameActionCheckSum;

    public void Handle(GameActionCheckSum checksSum)
    {
        _gameActionCheckSum = checksSum;

        MakeSacrificeGameAction makeSacrificeGameAction = checksSum.GameAction as MakeSacrificeGameAction;


        MakeSacrifice(checksSum.Player, makeSacrificeGameAction.GetCosts());
        HandleTravelling();
    }

    private void MakeSacrifice(Player player, List<IAccumulativePlayerStat> costs)
    {
        GameFlowManager.Instance.ExecuteMakeSacrificeEvent(EventTriggerSourceType.GameAction, player.PlayerNumber);
    }

    private void HandleTravelling()
    {
        ILocation constructionSiteLocation = _gameActionCheckSum.Location;
        if (constructionSiteLocation.LocationType == _gameActionCheckSum.Player.Location.LocationType) return; // If the player is already at the location, don't travel, don't subtract costs

        Player player = _gameActionCheckSum.Player;
        PlayerManager.Instance.GoToLocation(player, constructionSiteLocation.LocationType);
        player.SetGold(player.Gold.Value + TempConfiguration.TravellingGoldCost);
    }
}
