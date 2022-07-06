using UnityEngine;

public static class ColourUtility
{
    public static Color GetColour(ColourType colourType)
    {
        switch (colourType)
        {
            case ColourType.GreenBright:
                return new Color(145f / 255f, 246f / 255f, 70f / 255f);
            case ColourType.Empty:
                return Color.white;
            case ColourType.ErrorRed:
                return new Color(200f / 255f, 0, 0);
            case ColourType.GrayedOut:
                return new Color(94f / 255f, 94f / 255f, 94f / 255f); // dark gray
            case ColourType.SelectedBackground:
                //return new Color(243f / 255f, 133f / 255f, 27f / 255f); //  orange
                return new Color(0f / 255f, 117f / 255f, 107f / 255f); // green
            case ColourType.Player1:
                return new Color(4f / 255f, 89f / 255f, 117f / 255f); // dark blue
            case ColourType.Player2:
                return new Color(186f / 255f, 23f / 255f, 36f / 255f); // red
            case ColourType.Player3:
                //return new Color(24f / 255f, 130f / 255f, 99f / 255f); //  green
                //return new Color(0f / 255f, 117f / 255f, 107f / 255f); // green / turqoise
                return new Color(243f / 255f, 133f / 255f, 27f / 255f); //  orange
            default:
                Debug.LogError($"Colour {colourType} was not yet implemented");
                return Color.black;
        }
    }

    public static string GetHexadecimalColour(ColourType colourType)
    {
        switch (colourType)
        {
            case ColourType.Empty:
                return "#FFFFFF";
            case ColourType.ErrorRed:
                return "#C60000";
            case ColourType.SelectedBackground:
                return "#69FDFF";
            default:
                Debug.LogError($"Colour {colourType} was not yet implemented");
                return "#000000";
        }
    }
}


