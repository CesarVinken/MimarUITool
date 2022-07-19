using System;

public class BribeWorkerEvent : EventArgs
{
    public EventTriggerSourceType EventTriggerSourceType { get; private set; }
    public PlayerNumber Employer { get; private set; }
    public IWorker Worker { get; private set; }

    public BribeWorkerEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber playerNumber, IWorker worker)
    {
        EventTriggerSourceType = eventTriggerSourceType;
        Employer = playerNumber;
        Worker = worker;
    }
}
