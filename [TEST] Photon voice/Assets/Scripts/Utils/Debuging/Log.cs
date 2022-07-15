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
    public static void log(string s, int fontSize = 30)
    {
        debugText.text += $"{s} :{fontSize}:{color};".Interpolate() + "\n";
    }
}
