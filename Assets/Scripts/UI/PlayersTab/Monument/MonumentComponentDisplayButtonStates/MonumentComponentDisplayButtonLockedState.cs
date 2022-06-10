using UnityEngine.UI;


public class MonumentComponentDisplayButtonLockedState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(MonumentComponentListItem item, Image buttonBackground)
    {
        SetButtonColour(ColourType.GrayedOut, buttonBackground);
        SetLockImage(item);
    }

    protected override void SetButtonColour(ColourType colourType, Image buttonBackground)
    {
        buttonBackground.color = ColourUtility.GetColour(colourType);
    }

    protected override void SetLockImage(MonumentComponentListItem item)
    {
        item.GetLockGameObject().SetActive(true);
    }
}

