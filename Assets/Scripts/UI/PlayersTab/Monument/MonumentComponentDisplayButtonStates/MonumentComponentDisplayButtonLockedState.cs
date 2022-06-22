using UnityEngine.UI;


public class MonumentComponentDisplayButtonLockedState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(MonumentComponentListItem item, Image buttonBackground)
    {
        SetButtonColour(ColourType.GrayedOut, buttonBackground);
        SetIcon(item);
    }

    protected override void SetButtonColour(ColourType colourType, Image buttonBackground)
    {
        buttonBackground.color = ColourUtility.GetColour(colourType);
    }

    protected override void SetIcon(MonumentComponentListItem item)
    {
        item.SetIcon(AssetManager.Instance.GetMonumentComponentListItemIcon(this));
        item.GetLockGameObject().SetActive(true);
    }
}

