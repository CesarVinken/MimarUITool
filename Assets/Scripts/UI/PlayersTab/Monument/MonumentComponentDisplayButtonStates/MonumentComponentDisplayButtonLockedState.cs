using TMPro;
using UnityEngine.UI;


public class MonumentComponentDisplayButtonLockedState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer)
    {
        SetSubLabel(subLabel, monumentComponent, playersTabContainer);
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
    protected override void SetSubLabel(TextMeshProUGUI subLabel, MonumentComponent monumentComponent, PlayersTabContainer playersTabContainer)
    {
        subLabel.enabled = false;
    }
}

