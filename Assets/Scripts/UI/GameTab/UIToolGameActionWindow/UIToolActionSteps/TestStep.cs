using System.Collections.Generic;

public class TestStep : IUIToolGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IUIToolGameActionElement> _elements = new List<IUIToolGameActionElement>();

    public TestStep()
    {
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IUIToolGameActionElement> Initialise()
    {
        IUIToolGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        IUIToolGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        return _elements;
    }
    public List<IUIToolGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public void NextStep()
    {
        UIToolGameActionHandler.CurrentUIGameToolAction.NextStep();
    }
}
