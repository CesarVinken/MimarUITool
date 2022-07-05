
public class HireWorkerGameAction : IGameAction
{
    private GameActionType _gameActionType;

    public HireWorkerGameAction()
    {
        _gameActionType = GameActionType.HireWorker;
    }

    public GameActionType GetGameActionType()
    {
        return _gameActionType;
    }
}
