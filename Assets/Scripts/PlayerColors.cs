using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlayerColors
{
    private static readonly List<Color> Colors = new()
    {
        new Color(.9f, .2f, .2f),
        new Color(.1f, .1f, 1f),
        new Color(.1f, .5f, .2f),
        new Color(.9f, .4f, .9f),
        new Color(1f, .5f, .2f),
        new Color(1f, .9f, .4f),
        new Color(.2f, .2f, .2f),
        new Color(.9f, .9f, .9f),
        new Color(.6f, 0f, .6f),
        new Color(.5f, .3f, .2f),
        new Color(0f, 1f, 1f),
        new Color(.3f, .8f, .2f)
    };

    public static Color GetColor(PlayerColorType colorType)
    {
        return Colors[(int)colorType];
    }

    public static int TotalColorCount => Colors.Count;
}