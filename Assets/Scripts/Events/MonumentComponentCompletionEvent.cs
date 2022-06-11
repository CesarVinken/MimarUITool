using System;

public class MonumentComponentCompletionEvent : EventArgs
{
    public PlayerNumber AffectedPlayer { get; private set; }
    public MonumentComponent AffectedComponent { get; private set; }
    public bool IsCompleted { get; private set; }
    public MonumentComponentCompletionEvent(PlayerNumber affectedPlayer, MonumentComponent affectedComponent, bool isCompleted)
    {
        AffectedPlayer = affectedPlayer;
        AffectedComponent = affectedComponent;
        IsCompleted = isCompleted;
    }
}