using System.Collections.Generic;
using UnityEngine;

public class UIToolGameActionHandler
{
    public static UIToolGameActionHandler CurrentUIGameToolAction = null;
    private UIToolActionWindow _uiToolActionWindow;
    public UIToolGameActionAssetHandler UIToolGameActionAssetHandler { get; private set; }

    private List<IUIToolGameActionStep> _uiToolGameActionSteps = new List<IUIToolGameActionStep>();

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

        if(_uiToolActionWindow == null)
        {
            Debug.LogError($"Could not find UIToolActionWindow script on {uiToolActionWindowGO.name}");
        }

        AddStep(new TestStep());

        if(_uiToolGameActionSteps.Count == 0)
        {
            Debug.LogError($"We need at least 1 uiToolGameActionStep");
            return;
        }

        IUIToolGameActionStep firstStep = _uiToolGameActionSteps[0];

        _uiToolActionWindow.LoadStepUI(firstStep);
    }

    private void AddStep(IUIToolGameActionStep step)
    {
        Debug.Log($"Add step");
        _uiToolGameActionSteps.Add(step);
    }

    public void Complete()
    {
        CurrentUIGameToolAction = null;
    }

}


public class TestStep : IUIToolGameActionStep
{
    List<IUIToolGameActionElement> elements = new List<IUIToolGameActionElement>()
    {
    };

    public List<IUIToolGameActionElement> Initialise()
    {
        GameObject executeActionButtonPrefab = UIToolGameActionAssetHandler.Instance.GetExecuteActionButton();
        GameObject executeActionButton = GameObject.Instantiate(executeActionButtonPrefab);
        IUIToolGameActionElement executionButton = executeActionButton.GetComponent<IUIToolGameActionElement>();

        if(executionButton == null)
        {
            Debug.LogError($"could not find executionButton script on executionButton");
        }

        elements.Add(executionButton);

        return elements;
    }
}

public interface IUIToolGameActionStep
{
    List<IUIToolGameActionElement> Initialise();
}

public interface IUIToolGameActionElement
{
    Transform GetTransform();
    GameObject GetGameObject();
}

