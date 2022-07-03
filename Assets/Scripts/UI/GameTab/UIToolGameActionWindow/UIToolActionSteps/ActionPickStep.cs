using System.Collections.Generic;
using UnityEngine;

public class ActionPickStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; }
    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();

    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        IUIToolGameActionElement stepLabelElement = GameActionElementInitaliser.InitialiseLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = GameActionElementInitaliser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        // List here all the possible Actions

        // TODO: show but gray out actions that cannot be done.

        IUIToolGameActionElement hireWorkerActionElement = GameActionElementInitaliser.InitialiseActionSelectionTile(this, UIToolGameActionType.HireWorker);
        _elements.Add(hireWorkerActionElement);
        IUIToolGameActionElement expandStockpileActionElement = GameActionElementInitaliser.InitialiseActionSelectionTile(this, UIToolGameActionType.ExpandStockpile);
        _elements.Add(expandStockpileActionElement);


        return _elements;
    }

    public void NextStep()
    {
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