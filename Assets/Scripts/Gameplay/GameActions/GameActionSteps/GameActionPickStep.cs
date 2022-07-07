using System.Collections.Generic;
using UnityEngine;

public class GameActionPickStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    private IGameAction _selectedGameAction;

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

        Player gameActionInitiator = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.Player;

        if(gameActionInitiator == null)
        {
            Debug.LogError($"We could not find out who initiated thsi game action, but we should be able to find a player");
        }

        // List here all the possible Actions
        AddGameActionElement(gameActionInitiator, new HireWorkerGameAction());
        AddGameActionElement(gameActionInitiator, new ExpandStockpileGameAction());

        foreach (KeyValuePair<GameActionType, GameActionActionSelectionTileElement> item in _actionTileByActionType)
        {
            if (item.Value.IsAvailable)
            {
                _actionTileByActionType[item.Key].Select();
                _selectedGameAction = item.Value.GameAction;
                return _elements;
            }
        }

        Debug.LogWarning($"We have no available actions. We need a return scenario for this.");
        return _elements;
    }

    public void SelectAction(IGameAction gameAction)
    {
        if (gameAction.GetGameActionType() == _selectedGameAction.GetGameActionType()) return;

        IGameAction previouslySelectedActionType = _selectedGameAction;
        _actionTileByActionType[previouslySelectedActionType.GetGameActionType()].Deselect(); // Deselect the current

        _selectedGameAction = gameAction;
        _actionTileByActionType[_selectedGameAction.GetGameActionType()].Select();
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

        GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum.WithActionType(_selectedGameAction);
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    private void AddGameActionElement(Player gameActionInitiator, IGameAction gameAction)
    {
        GameActionActionSelectionTileElement expandStockpileActionElement = GameActionElementInitialiser.InitialiseActionSelectionTile(this, gameAction);
        bool isAvailable = expandStockpileActionElement.GameAction.IsAvailableForPlayer(gameActionInitiator);

        if (!isAvailable)
        {
            expandStockpileActionElement.MakeUnavailable();
        }

        _actionTileByActionType.Add(expandStockpileActionElement.GameAction.GetGameActionType(), expandStockpileActionElement);
        _elements.Add(expandStockpileActionElement);
    }
}

