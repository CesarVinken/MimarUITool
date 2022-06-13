using System;

public class MonumentComponentCompletionStateChangeEvent : EventArgs
{
    public PlayerNumber AffectedPlayer { get; private set; }
    public MonumentComponent AffectedComponent { get; private set; }
    public bool IsCompleted { get; private set; }
    public MonumentComponentCompletionStateChangeEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, bool isCompleted)
    {
        AffectedPlayer = affectedPlayer;
        AffectedComponent = affectedComponent;
        IsCompleted = isCompleted;
    }
}