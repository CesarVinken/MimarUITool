using System.Collections.Generic;

public interface IGameActionStep
{
    public int StepNumber { get; }

    List<IGameActionElement> Initialise();
    List<IGameActionElement> GetUIElements();
    void NextStep();
}
