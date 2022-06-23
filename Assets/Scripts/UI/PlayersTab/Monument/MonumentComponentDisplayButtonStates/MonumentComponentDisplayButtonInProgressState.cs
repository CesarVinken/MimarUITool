using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonumentComponentDisplayButtonInProgressState : MonumentComponentDisplayButtonState
{
    public override void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer)
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
        item.SetIcon(AssetManager.Instance.GetMonumentComponentListItemIcon(this));
        item.GetLockGameObject().SetActive(true);
    }

    protected override void SetSubLabel(TextMeshProUGUI subLabel, MonumentComponent monumentComponent, PlayersTabContainer playersTabContainer)
    {
        Player player = PlayerManager.Instance.Players[playersTabContainer.CurrentPlayerTab.PlayerNumber];
        float remainingLabourTime = monumentComponent.RemainingLabourTime;

        string subLabelText = "";

        subLabelText += $"{AssetManager.Instance.GetInlineIcon(InlineIconType.LabourTime)} {remainingLabourTime}    ";

        subLabel.text = subLabelText;
        subLabel.enabled = true;
    }
}