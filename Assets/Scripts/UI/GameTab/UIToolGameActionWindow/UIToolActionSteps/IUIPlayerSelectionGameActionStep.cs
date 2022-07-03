
// Interface used for any step that involves the selection of a player (either as initiator or target)
public interface IUIPlayerSelectionGameActionStep
{
    void SelectPlayer(PlayerNumber playerNumber);
}