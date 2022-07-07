using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MonumentComponentDisplayButtonAvailableState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer
        )
    {
        SetSubLabel(subLabel, monumentComponent, playersTabContainer);
        SetButtonColour(ColourType.Empty, buttonBackground);
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
        Player player = PlayerManager.Instance.Players[playersTabContainer.CurrentPlayerTab.PlayerNumber];

        List<IResource> resourceCosts = monumentComponent.MonumentComponentBlueprint.ResourceCosts;

        string subLabelText = "";


        for (int i = 0; i < resourceCosts.Count; i++)
        {
            InlineIconType inlineIconType = resourceCosts[i].GetInlineIconType();
            subLabelText += $"{AssetManager.Instance.GetInlineIcon(inlineIconType)} {resourceCosts[i].Value}    ";
        }

        subLabel.text = subLabelText;
        subLabel.enabled = true;
    }

}