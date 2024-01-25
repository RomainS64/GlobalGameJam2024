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

    public bool CompareColor(SColor color)
    {
        return this.Color == color.Color;
    }
}
public class ColorProperty : IJesterProperty
{
    private SColor color;
    public IJesterPropertyInfo Info { get => color; set => color = (SColor)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        return color.CompareColor((SColor)_info);
    }
}
