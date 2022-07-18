using System;

public class ExtendWorkerContractEvent : EventArgs
{
    public EventTriggerSourceType EventTriggerSourceType { get; private set; }
    public PlayerNumber Employer { get; private set; }
    public IWorker Worker { get; private set; }
    public int NewContractLength { get; private set; }

    public ExtendWorkerContractEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber playerNumber, IWorker worker, int newContractLength)
    {
        EventTriggerSourceType = eventTriggerSourceType;
        Employer = playerNumber;
        Worker = worker;
        NewContractLength = newContractLength;
    }
}
