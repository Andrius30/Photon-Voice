using TMPro;
using UnityEngine;

public class Log
{
    static Color color = new Color(0, 255, 97, 255);
    static Color defaultColor;
    static TextMeshProUGUI debugText;

    public static void InitLogger(TextMeshProUGUI text)
    {
        debugText = text;
        defaultColor = color;
    }
    public static void SetColor(Color col)
    {
        if (col != color)
        {
            color = col;
        }
    }
    public static void ResetColor() => color = defaultColor;
    public static void log(string s, bool newLine = true, int fontSize = 30)
    {
        if (newLine)
        {
            debugText.text += $"{s} :{fontSize}:{color};".Interpolate() + "\n";
        }
        else
        {
            debugText.text = $"{s} :{fontSize}:{color};".Interpolate();
        }
    }
    public static void log(string s, string secondraryText, Color sColor, bool newLine = true, int fontSize = 30)
    {
        if (newLine)
        {
            debugText.text += $"{s} :{fontSize}:{color};".Interpolate() + $"{secondraryText} :{fontSize}:{sColor};".Interpolate() + "\n";
        }
        else
        {
            debugText.text = $"{s} :{fontSize}:{color};".Interpolate() + $"{secondraryText} :{fontSize}:{sColor};".Interpolate();
        }
    }
}
