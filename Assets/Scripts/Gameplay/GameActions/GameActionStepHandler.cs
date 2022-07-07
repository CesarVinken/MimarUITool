using System.Collections.Generic;
using UnityEngine;

public class GameActionStepHandler
{
    public static GameActionStepHandler CurrentGameActionSequence = null;
    private GameActionWindow _gameActionWindow;
    public GameActionAssetHandler GameActionAssetHandler { get; private set; }

    private List<IGameActionStep> _gameActionSteps = new List<IGameActionStep>();
    private IGameActionStep _currentGameActionStep = null;

    public GameActionCheckSum GameActionCheckSum;
    public GameActionStepHandler()
    {
        Debug.Log($"Commence new game tool action");
        CurrentGameActionSequence = this;
        GameActionCheckSum = new GameActionCheckSum();

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
        GameFlowManager.Instance.AddPlannedGameAction(CurrentGameActionSequence.GameActionCheckSum);
        PlayerManager.Instance.UpdatePlayerMove(CurrentGameActionSequence.GameActionCheckSum.Player, false);

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
