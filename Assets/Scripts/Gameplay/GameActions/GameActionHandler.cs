using System.Collections.Generic;
using UnityEngine;

public class GameActionHandler
{
    public static GameActionHandler CurrentGameActionSequence = null;
    private GameActionWindow _gameActionWindow;
    public GameActionAssetHandler GameActionAssetHandler { get; private set; }

    private List<IGameActionStep> _gameActionSteps = new List<IGameActionStep>();
    private IGameActionStep _currentGameActionStep = null;

    public GameActionCheckSum GameActionCheckSum;
    private GameActionChecksumHandler _checksumHandler;

    public GameActionHandler()
    {
        Debug.Log($"Commence new game tool action");
        CurrentGameActionSequence = this;
        GameActionCheckSum = new GameActionCheckSum();
        _checksumHandler = new GameActionChecksumHandler();

        GameObject gameActionWindowPrefab = GameActionAssetHandler.Instance.GetGameActionWindowPrefab();
        GameObject gameActionWindowGO = GameObject.Instantiate(gameActionWindowPrefab);

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameActionWindowGO.transform.SetParent(gameTabContainer.transform);
        gameActionWindowGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        _gameActionWindow = gameActionWindowGO.GetComponent<GameActionWindow>();

        if (_gameActionWindow == null)
        {
            Debug.LogError($"Could not find gameActionWindowGO script on {gameActionWindowGO.name}");
        }

        AddStep(new PlayerPickStep());
        AddStep(new GameActionPickStep());

        if (_gameActionSteps.Count == 0)
        {
            Debug.LogError($"We need at least 1 gameActionStep");
            return;
        }

        NextStep();
    }

    public List<IGameActionStep> GetSteps()
    {
        return _gameActionSteps;
    }

    public void AddStep(IGameActionStep step)
    {
        _gameActionSteps.Add(step);
    }

    public void NextStep()
    {

        if (_currentGameActionStep == null)
        {
            _currentGameActionStep = _gameActionSteps[0];
        }
        else
        {
            int nextStepNumber = _currentGameActionStep.StepNumber + 1;

            if (nextStepNumber > _gameActionSteps.Count)
            {
                CompleteSequence();
                return;
            }
            else
            {
                _currentGameActionStep = _gameActionSteps[nextStepNumber - 1];
                _gameActionWindow.EmptyWindowUI();
            }
        }
        _gameActionWindow.LoadStepUI(_currentGameActionStep);
    }

    public IGameActionStep GetCurrentStep()
    {
        return _currentGameActionStep;
    }

    public void CompleteSequence()
    {
        _checksumHandler.HandleGameAction();

        CloseGameActionWindow();
    }

    public void CloseGameActionWindow()
    {
        CurrentGameActionSequence = null;
        _currentGameActionStep = null;
        _gameActionSteps.Clear();

        _gameActionWindow.DestroyWindow();
    }
}

public class GameActionChecksumHandler
{
    public void HandleGameAction()
    {
        GameActionCheckSum gameActionCheckSum = GameActionHandler.CurrentGameActionSequence.GameActionCheckSum;
        GameActionType gameActionType = gameActionCheckSum.ActionType;

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        gameTabContainer.UpdatePlayerMove(gameActionCheckSum.Player, false);


        switch (gameActionType)
        {
            case GameActionType.HireWorker:

                Debug.Log($"EXECUTE ACTION ---- Hire a worker for {gameActionCheckSum.Player.Name}");
                break;
            case GameActionType.ExpandStockpile:
                Debug.Log($"Expand the stockpile for {gameActionCheckSum.Player.Name}");
                break;
            default:

                Debug.LogError($"The game action type {gameActionType} is not implemented");
                break;
        }
    }
}