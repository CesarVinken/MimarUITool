using System;

public class HireWorkerEvent : EventArgs
{
    public EventTriggerSourceType EventTriggerSourceType { get; private set; }
    public PlayerNumber Employer { get; private set; }
    public IWorker Worker { get; private set; }
    public int ContractLength { get; private set; }

    public HireWorkerEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber playerNumber, IWorker worker, int contractLength)
    {
        EventTriggerSourceType = eventTriggerSourceType;
        Employer = playerNumber;
        Worker = worker;
        ContractLength = contractLength;
    }

}
