using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerColor
{
    Red, Blue, Green,
    Pink, Orange, Yellow,
    Black, White, Purple,
    Brown, Cyan, Lime
}
public class PlayerColor
{
    private static List<Color> colors = new List<Color>()
    {
        new Color(1f, 0f, 0f),
        new Color(0f, 0.1f, 1f),
        new Color(0f, 0.6f, 0f),
        new Color(1f, 0.3f, 0.9f),
        new Color(1f, 0.4f, 0f),
        new Color(1f, 0.9f, 0.1f),
        new Color(0.2f, 0.2f, 0.2f),
        new Color(0.9f, 1f, 1f),
        new Color(0.6f, 0f, 0.6f),
        new Color(0.7f, 0.2f, 0f),
        new Color(0f, 1f, 1f),
        new Color(0.1f, 1f, 0.1f),
    };

    public static Color GetColor(EPlayerColor playerColor)
    {
        return colors[(int)playerColor];
    }

    public static Color red { get { return colors[(int)EPlayerColor.Red]; } }
    public static Color blue { get { return colors[(int)EPlayerColor.Blue]; } }
    public static Color green { get { return colors[(int)EPlayerColor.Green]; } }
    public static Color pink { get { return colors[(int)EPlayerColor.Pink]; } }
    public static Color orange { get { return colors[(int)EPlayerColor.Orange]; } }
    public static Color yellow { get { return colors[(int)EPlayerColor.Yellow]; } }
    public static Color black { get { return colors[(int)EPlayerColor.Black]; } }
    public static Color white { get { return colors[(int)EPlayerColor.White]; } }
    public static Color purple { get { return colors[(int)EPlayerColor.Purple]; } }
    public static Color brown { get { return colors[(int)EPlayerColor.Brown]; } }
    public static Color cyan { get { return colors[(int)EPlayerColor.Cyan]; } }
    public static Color lime { get { return colors[(int)EPlayerColor.Lime]; } }
}
