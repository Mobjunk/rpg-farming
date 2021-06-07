using System;
using UnityEngine;

public enum AnchorsPresets
{
    TOP_LEFT,
    TOP,
    TOP_RIGHT,
    RIGHT,
    BOTTOM_RIGHT,
    BOTTOM,
    BOTTOM_LEFT,
    LEFT,
    CENTER
}

public static class AnchorsPresetManager
{

    public static Vector2 GetAnchor(AnchorsPresets anchor)
    {
        switch (anchor)
        {
            case AnchorsPresets.TOP_LEFT:
                return new Vector2(0, 1);
            case AnchorsPresets.TOP:
                return new Vector2(0.5f, 1);
            case AnchorsPresets.TOP_RIGHT:
                return new Vector2(1, 1);
            case AnchorsPresets.RIGHT:
                return new Vector2(1, 0.5f);
            case AnchorsPresets.BOTTOM_RIGHT:
                return new Vector2(1, 0);
            case AnchorsPresets.BOTTOM:
                return new Vector2(0.5f, 0);
            case AnchorsPresets.BOTTOM_LEFT:
                return new Vector2(0, 0);
            case AnchorsPresets.LEFT:
                return new Vector2(0, 0.5f);
            case AnchorsPresets.CENTER:
                return new Vector2(0.5f, 0.5f);
            default:
                throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
        }
    }
}
