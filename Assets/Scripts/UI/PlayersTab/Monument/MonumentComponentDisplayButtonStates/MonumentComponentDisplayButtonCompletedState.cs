using UnityEngine.UI;


public class MonumentComponentDisplayButtonCompletedState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(MonumentComponentListItem item, Image buttonBackground)
    {
        SetButtonColour(ColourType.SelectedBackground, buttonBackground);
        SetIcon(item);
    }

    protected override void SetButtonColour(ColourType colourType, Image buttonBackground)
    {
        buttonBackground.color = ColourUtility.GetColour(colourType);
    }

    protected override void SetIcon(MonumentComponentListItem item)
    {
        item.GetLockGameObject().SetActive(false);
    }
}