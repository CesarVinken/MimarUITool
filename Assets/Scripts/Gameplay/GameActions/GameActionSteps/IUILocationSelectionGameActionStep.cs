// Interface used for any step that involves the selection of a location (either as initiator or target)
public interface IUILocationSelectionGameActionStep
{
    void SelectLocation(LocationType locationType);
}