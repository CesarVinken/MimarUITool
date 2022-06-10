using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonumentComponentDisplayButtonState
{
    public abstract void UpdateUIForButtonState(MonumentComponentListItem item, Image buttonBackground);
    protected abstract void SetButtonColour(ColourType colourType, Image buttonBackground);
    protected abstract void SetLockImage(MonumentComponentListItem item);
}
