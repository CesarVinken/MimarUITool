public class ExpandStockpileGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public ExpandStockpileGameAction()
    {
        _gameActionType = GameActionType.ExpandStockpile;
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }
}
