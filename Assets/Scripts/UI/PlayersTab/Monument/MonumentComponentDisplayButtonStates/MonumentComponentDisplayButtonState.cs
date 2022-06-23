using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonumentComponentDisplayButtonState
{
    public abstract void UpdateUIForButtonState(
        MonumentComponentListItem item,
        Image buttonBackground,
        TextMeshProUGUI subLabel,
        MonumentComponent monumentComponent,
        PlayersTabContainer playersTabContainer
    );
    protected abstract void SetButtonColour(ColourType colourType, Image buttonBackground);
    protected abstract void SetIcon(MonumentComponentListItem item);
    protected abstract void SetSubLabel(TextMeshProUGUI subLabel, MonumentComponent monumentComponent, PlayersTabContainer playersTabContainer);
}
