using System;
using UnityEngine;

public static class DangerLevelExtensions
{
    public static Color GetColor(this DangerLevel dangerLevel)
    {
        switch (dangerLevel)
        {
            case DangerLevel.None:
                return Color.white;

            case DangerLevel.Pacific:
                return Color.blue;

            case DangerLevel.Easy:
                return Color.green;

            case DangerLevel.Normal:
                return new Color(0.5f, 1f, 0f); // green-yellow

            case DangerLevel.Hard:
                return new Color(1f, 0.5f, 0f); // orange

            case DangerLevel.Massive:
                return Color.red;

            case DangerLevel.Impossible:
                return new Color(1f, 0f, 0.5f); // red-purple

            default:
                throw new ArgumentOutOfRangeException(nameof(dangerLevel), dangerLevel, null);
        }
    }
}