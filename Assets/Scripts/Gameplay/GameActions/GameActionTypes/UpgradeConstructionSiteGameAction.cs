using UnityEngine;
public class UpgradeConstructionSiteGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public UpgradeConstructionSiteGameAction()
    {
        _gameActionType = GameActionType.UpgradeConstructionSite;
    }

    public string GetName()
    {
        return "Upgrade Construction Site";
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }

    public bool IsAvailableForPlayer(Player player)
    {
        Debug.Log($"TODO check if UpgradeConstructionSite should be available for the player");
        // check if player is at maximum upgrade level
        // check if player has resources
        // check if player has money to travel to construction site (if needed)
        return true;
    }
}
