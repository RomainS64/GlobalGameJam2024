using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMask
{
    Sad,
   Happy,
   Angry,
   Surprised,
   Lover
}
public struct SMask : IJesterPropertyInfo
{
    public SMask(EMask _mask = EMask.Sad)
    {
        Mask = _mask;
    }

    public EMask Mask;

    public bool CompareMask(SMask mask)
    {
        return this.Mask == mask.Mask;
    }
}
public class MaskProperty : IJesterProperty
{
    private SMask mask;
    public IJesterPropertyInfo Info { get => mask; set => mask = (SMask)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SMask)
        {
            return mask.CompareMask((SMask)_info);
        }
        else
        {
            return false;
        }
    }
}
