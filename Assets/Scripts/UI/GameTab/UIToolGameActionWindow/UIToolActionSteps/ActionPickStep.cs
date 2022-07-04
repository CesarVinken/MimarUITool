using System.Collections.Generic;
using UnityEngine;

public class ActionPickStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();
    private UIToolGameActionType _selectedActionType;

    private Dictionary<UIToolGameActionType, GameActionActionSelectionTileElement> _actionTileByActionType = new Dictionary<UIToolGameActionType, GameActionActionSelectionTileElement>();

    public ActionPickStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        _actionTileByActionType.Clear();

        IUIToolGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        // List here all the possible Actions

        // TODO: show but gray out actions that cannot be done.

        GameActionActionSelectionTileElement hireWorkerActionElement = GameActionElementInitialiser.InitialiseActionSelectionTile(this, UIToolGameActionType.HireWorker);
        _actionTileByActionType.Add(hireWorkerActionElement.GameActionType, hireWorkerActionElement);
        _elements.Add(hireWorkerActionElement);
        GameActionActionSelectionTileElement expandStockpileActionElement = GameActionElementInitialiser.InitialiseActionSelectionTile(this, UIToolGameActionType.ExpandStockpile);
        _actionTileByActionType.Add(expandStockpileActionElement.GameActionType, expandStockpileActionElement);
        _elements.Add(expandStockpileActionElement);

        _actionTileByActionType[UIToolGameActionType.HireWorker].Select();

        return _elements;
    }

    public void SelectAction(UIToolGameActionType actionType)
    {
        if (actionType == _selectedActionType) return;

        UIToolGameActionType previouslySelectedActionType = _selectedActionType;
        _actionTileByActionType[previouslySelectedActionType].Deselect(); // Deselect the current

        _selectedActionType = actionType;
        _actionTileByActionType[_selectedActionType].Select();
    }

    public void NextStep()
    {
        UIToolGameActionHandler.CurrentUIGameToolAction.AddStep(new CheckoutStep());

        UIToolGameActionHandler.CurrentUIGameToolAction.GameActionCheckSum.WithActionType(_selectedActionType);
        UIToolGameActionHandler.CurrentUIGameToolAction.NextStep();
    }
}


public class GameActionPlayerDataElement : IGameActionDataElement
{
    public Player Player { get; private set; }
}

public interface IGameActionDataElement
{

}