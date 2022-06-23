using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class MonumentComponentDisplayButtonUnaffordableState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer)
    {
        SetButtonColour(ColourType.Empty, buttonBackground);
        SetIcon(item); 
        SetSubLabel(subLabel, monumentComponent, playersTabContainer);
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

            ResourceType resourceType = resourceCosts[i].GetResourceType();
            bool canAffordResource = player.Resources[resourceType].Amount >= resourceCosts[i].Amount;
            
            if (canAffordResource)
            {
                subLabelText += $"{AssetManager.Instance.GetInlineIcon(inlineIconType)} {resourceCosts[i].Amount}    ";
            }
            else
            {
                subLabelText += $"{AssetManager.Instance.GetInlineIcon(inlineIconType)} <color={ColourUtility.GetHexadecimalColour(ColourType.ErrorRed)}>{resourceCosts[i].Amount}</color>    ";
            }
        }

        subLabel.text = subLabelText;
        subLabel.enabled = true;
    }
}