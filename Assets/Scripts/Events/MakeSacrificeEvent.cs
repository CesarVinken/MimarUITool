using System;

public class MakeSacrificeEvent : EventArgs
{
    public EventTriggerSourceType EventTriggerSourceType { get; private set; }
    public PlayerNumber PlayerNumber { get; private set; }

    public MakeSacrificeEvent(EventTriggerSourceType eventTriggerSourceType, PlayerNumber playerNumber)
    {
        EventTriggerSourceType = eventTriggerSourceType;
        PlayerNumber = playerNumber;
    }
}
