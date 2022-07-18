using System;

public class MonumentComponentCompletionStateChangeEvent : EventArgs
{
    public PlayerNumber AffectedPlayer { get; private set; }
    public MonumentComponent AffectedComponent { get; private set; }
    public MonumentComponentState State { get; private set; }
    public MonumentComponentCompletionStateChangeEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, MonumentComponentState state)
    {
        AffectedPlayer = affectedPlayer;
        AffectedComponent = affectedComponent;
        State = state;
    }
}
