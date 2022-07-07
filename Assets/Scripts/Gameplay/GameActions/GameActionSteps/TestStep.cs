using System.Collections.Generic;

public class TestStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();

    public TestStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> Initialise()
    {
        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        return _elements;
    }
    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public void NextStep()
    {
        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }
}
