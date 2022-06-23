using TMPro;
using UnityEngine.UI;


public class MonumentComponentDisplayButtonCompletedState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer)
    {
        SetSubLabel(subLabel, monumentComponent, playersTabContainer);
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

    protected override void SetSubLabel(TextMeshProUGUI subLabel, MonumentComponent monumentComponent, PlayersTabContainer playersTabContainer)
    {
        subLabel.enabled = false;
    }
}