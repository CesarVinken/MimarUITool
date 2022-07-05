using System.Collections.Generic;
using UnityEngine;

public class GameActionPickStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private GameActionType _selectedActionType;

    private Dictionary<GameActionType, GameActionActionSelectionTileElement> _actionTileByActionType = new Dictionary<GameActionType, GameActionActionSelectionTileElement>();

    public GameActionPickStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IGameActionElement> Initialise()
    {
        _actionTileByActionType.Clear();

        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        // List here all the possible Actions

        // TODO: show but gray out actions that cannot be done.

        GameActionActionSelectionTileElement hireWorkerActionElement = GameActionElementInitialiser.InitialiseActionSelectionTile(this, GameActionType.HireWorker);
        _actionTileByActionType.Add(hireWorkerActionElement.GameActionType, hireWorkerActionElement);
        _elements.Add(hireWorkerActionElement);
        GameActionActionSelectionTileElement expandStockpileActionElement = GameActionElementInitialiser.InitialiseActionSelectionTile(this, GameActionType.ExpandStockpile);
        _actionTileByActionType.Add(expandStockpileActionElement.GameActionType, expandStockpileActionElement);
        _elements.Add(expandStockpileActionElement);

        _actionTileByActionType[GameActionType.HireWorker].Select();

        return _elements;
    }

    public void SelectAction(GameActionType actionType)
    {
        if (actionType == _selectedActionType) return;

        GameActionType previouslySelectedActionType = _selectedActionType;
        _actionTileByActionType[previouslySelectedActionType].Deselect(); // Deselect the current

        _selectedActionType = actionType;
        _actionTileByActionType[_selectedActionType].Select();
    }

    public void NextStep()
    {
        GameActionHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

        GameActionHandler.CurrentGameActionSequence.GameActionCheckSum.WithActionType(_selectedActionType);
        GameActionHandler.CurrentGameActionSequence.NextStep();
    }
}


public class GameActionPlayerDataElement : IGameActionDataElement
{
    public Player Player { get; private set; }
}

public interface IGameActionDataElement
{

}