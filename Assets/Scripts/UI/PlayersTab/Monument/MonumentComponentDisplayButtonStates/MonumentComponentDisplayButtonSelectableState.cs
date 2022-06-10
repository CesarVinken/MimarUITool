using UnityEngine.UI;

public class MonumentComponentDisplayButtonSelectableState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(MonumentComponentListItem item, Image buttonBackground)
    {
        SetButtonColour(ColourType.Empty, buttonBackground);
        SetLockImage(item);
    }

    protected override void SetButtonColour(ColourType colourType, Image buttonBackground)
    {
        buttonBackground.color = ColourUtility.GetColour(colourType);
    }

    protected override void SetLockImage(MonumentComponentListItem item)
    {
        item.GetLockGameObject().SetActive(false);
    }
}