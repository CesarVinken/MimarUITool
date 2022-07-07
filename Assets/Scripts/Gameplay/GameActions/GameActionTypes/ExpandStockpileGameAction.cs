using UnityEngine;
public class ExpandStockpileGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public ExpandStockpileGameAction()
    {
        _gameActionType = GameActionType.ExpandStockpile;
    }

    public string GetName()
    {
        return "Expand stockpile";
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }

    public bool IsAvailableForPlayer(Player player)
    {
        Debug.Log($"TODO check if Expand stockpile should be available for the player");
        // check if player is at maximum upgrade level
        // check if player has resources
        // check if player has money to travel to construction site (if needed)
        return true;
    }
}
