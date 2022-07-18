using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHiringTermStep : IGameActionStep
{
    public int StepNumber { get; private set; } = -1;
    private List<IGameActionElement> _elements = new List<IGameActionElement>();
    public WorkerActionType WorkerActionType { get; private set; }

    private GameActionMainContentTextBlockElement _contentElement;
    private int _addedContractDuration = 0;

    public SetHiringTermStep(WorkerActionType workerActionType)
    {
        WorkerActionType = workerActionType;
        StepNumber = GameActionUtility.CalculateStepNumber();
    }

    public List<IGameActionElement> Initialise()
    {

        Debug.LogWarning($"Initialise");
        IGameActionElement stepLabelElement = GameActionElementInitialiser.InitialiseTitleLabel(this);
        _elements.Add(stepLabelElement);

        string inputLabel = GetInputLabelText();
        GameActionInputFieldElement inputFieldElement = GameActionElementInitialiser.InitialiseInputField(this, "Length in turns:") as GameActionInputFieldElement;
        _elements.Add(inputFieldElement);

        IGameActionElement contentLabelElement = GameActionElementInitialiser.InitialiseMainContentLabel(this, "");
        _contentElement = contentLabelElement as GameActionMainContentTextBlockElement;
        _elements.Add(contentLabelElement);

        IGameActionElement nextStepButtonElement = GameActionElementInitialiser.InitialiseNextStepButton(this);
        _elements.Add(nextStepButtonElement);

        int defaultTurns = 3;
        inputFieldElement.SetInputValue(defaultTurns);

        return _elements;
    }

    private string GetInputLabelText()
    {
        if(WorkerActionType == WorkerActionType.ExtendContract)
        {
            return "Turns to extend:";
        }
        else
        {
            return "Length in turns: ";
        }
    }

    public List<IGameActionElement> GetUIElements()
    {
        return _elements;
    }

    public void NextStep()
    {
        GameActionCheckSum checksum = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum;
        WorkerGameAction workerGameAction = checksum.GameAction as WorkerGameAction;

        if (workerGameAction == null)
        {
            Debug.LogError($"Could not parse the GameAction {checksum.GameAction.GetGameActionType()} as a WorkerGameAction");
        }
        workerGameAction.WithContractLength(_addedContractDuration);
        // TODO SET CONTRACT TERMS
        GameActionStepHandler.CurrentGameActionSequence.AddStep(new CheckoutStep());

        GameActionStepHandler.CurrentGameActionSequence.NextStep();
    }

    public void OnInputFieldValueChanged(string extraHiringTurns)
    {
        if (!int.TryParse(extraHiringTurns, out int extraHiringTurnsInt))
        {
            Debug.LogError($"Could not parse {extraHiringTurns} as an int");
            return;
        }

        string contentText = "";
        GameActionCheckSum checksum = GameActionStepHandler.CurrentGameActionSequence.GameActionCheckSum;
        string playerName = checksum.Player.Name;
        _addedContractDuration = extraHiringTurnsInt;

        WorkerGameAction workerGameAction = checksum.GameAction as WorkerGameAction;

        if (workerGameAction == null)
        {
            Debug.LogError($"Could not parse the GameAction {checksum.GameAction.GetGameActionType()} as a WorkerGameAction");
        }

        int remainingContractTurns = workerGameAction.GetWorker().ServiceLength;
        bool multipleTurnsRemain = remainingContractTurns > 1;
        string turnString = multipleTurnsRemain ? "turns" : "turn";

        switch (WorkerActionType)
        {
            case WorkerActionType.ExtendContract:

                if (remainingContractTurns > 1)
                {
                    contentText = $"In addition to the remaining {remainingContractTurns} {turnString}, {playerName} will hire this worker for {extraHiringTurns} more turns. " +
                        $"In total: {remainingContractTurns + extraHiringTurnsInt} turns.";
                }
                break;
            case WorkerActionType.Hire:
                contentText = $"{playerName} will hire this worker for {extraHiringTurns} {turnString}";
                break;
            default:
                new NotImplementedException("WorkerActionType", WorkerActionType.ToString());
                break;
        }
        _contentElement.SetText(contentText);
    }
}
