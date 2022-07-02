using System.Collections.Generic;
using UnityEngine;

public class UIToolGameActionHandler
{
    public static UIToolGameActionHandler CurrentUIGameToolAction = null;
    private UIToolActionWindow _uiToolActionWindow;
    public UIToolGameActionAssetHandler UIToolGameActionAssetHandler { get; private set; }

    private List<IUIToolGameActionStep> _uiToolGameActionSteps = new List<IUIToolGameActionStep>();
    private IUIToolGameActionStep _currentGameActionStep = null;

    public UIToolGameActionHandler()
    {
        Debug.Log($"Commence new game tool action");
        CurrentUIGameToolAction = this;

        GameObject uiToolActionWindowPrefab = UIToolGameActionAssetHandler.Instance.GetUIToolActionWindowPrefab();
        GameObject uiToolActionWindowGO = GameObject.Instantiate(uiToolActionWindowPrefab);

        GameTabContainer gameTabContainer = NavigationManager.Instance.GetMainTabContainer(MainTabType.GameTab) as GameTabContainer;
        uiToolActionWindowGO.transform.SetParent(gameTabContainer.transform);
        uiToolActionWindowGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        _uiToolActionWindow = uiToolActionWindowGO.GetComponent<UIToolActionWindow>();

        if (_uiToolActionWindow == null)
        {
            Debug.LogError($"Could not find UIToolActionWindow script on {uiToolActionWindowGO.name}");
        }

        AddStep(new PlayerPickStep());
        AddStep(new TestStep());
        AddStep(new TestStep());

        if (_uiToolGameActionSteps.Count == 0)
        {
            Debug.LogError($"We need at least 1 uiToolGameActionStep");
            return;
        }

        NextStep();
    }

    public List<IUIToolGameActionStep> GetSteps()
    {
        return _uiToolGameActionSteps;
    }

    private void AddStep(IUIToolGameActionStep step)
    {
        Debug.Log($"Add step {step.StepNumber}");
        _uiToolGameActionSteps.Add(step);
    }

    public void NextStep()
    {
        if (_currentGameActionStep == null)
        {
            _currentGameActionStep = _uiToolGameActionSteps[0];
        }
        else
        {
            int nextStepNumber = _currentGameActionStep.StepNumber + 1;

            if (nextStepNumber > _uiToolGameActionSteps.Count)
            {
                Complete();
                return;
            }
            else
            {
                _currentGameActionStep = _uiToolGameActionSteps[nextStepNumber - 1];
                _uiToolActionWindow.EmptyWindowUI();
            }
        }
        _uiToolActionWindow.LoadStepUI(_currentGameActionStep);
    }

    public void Complete()
    {
        Debug.Log($"complete");
        CurrentUIGameToolAction = null;
        _currentGameActionStep = null;
        _uiToolGameActionSteps.Clear();

        _uiToolActionWindow.DestroyWindow();
    }
}


public class TestStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; }
    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();

    public TestStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        IUIToolGameActionElement stepLabelElement = ToolActionElementInitaliser.InitialiseLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = ToolActionElementInitaliser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        return _elements;
    }
    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }
}

public class PlayerPickStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; }

    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();

    public PlayerPickStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        IUIToolGameActionElement stepLabelElement = ToolActionElementInitaliser.InitialiseLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = ToolActionElementInitaliser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        IUIToolGameActionElement player1TileElement = ToolActionElementInitaliser.InitialisePlayerSelectionTile(this);
        _elements.Add(player1TileElement);
        IUIToolGameActionElement player2TileElement = ToolActionElementInitaliser.InitialisePlayerSelectionTile(this);
        _elements.Add(player2TileElement);
        IUIToolGameActionElement player3TileElement = ToolActionElementInitaliser.InitialisePlayerSelectionTile(this);
        _elements.Add(player3TileElement);

        return _elements;
    }

    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }
}