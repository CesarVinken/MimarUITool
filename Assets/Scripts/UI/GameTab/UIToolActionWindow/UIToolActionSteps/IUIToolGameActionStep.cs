using System.Collections.Generic;

public interface IUIToolGameActionStep
{
    public int StepNumber { get; }

    List<IUIToolGameActionElement> Initialise();
}
