using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Blue,
    Yellow,
    Red,
    Green,
    Purple
}
public struct SColor : IJesterPropertyInfo
{
    public SColor(EColor _color)
    {
        Color = _color;
    }

    public EColor Color;

    public static bool operator ==(SColor color1, SColor color2)
    {
        if (color1 == null || color2 == null)
        {
            return false;
        }
        return color1.Color == color2.Color;
    }

    public static bool operator !=(SColor color1, SColor color2)
    {
        return !(color1 == color2);
    }
}
public class ColorProperty : IJesterProperty
{
    private SColor color;
    public IJesterPropertyInfo Info { get => color; set => color = (SColor)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        return color == (SColor)_info;
    }
}
