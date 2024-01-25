using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EDanceOrFall
{
    Dance,
    Fall
}
public struct SDanceOrFall : IJesterPropertyInfo
{
    public SDanceOrFall(EDanceOrFall _action)
    {
        Action = _action;
    }

    public EDanceOrFall Action;

    public bool CompareAction(SDanceOrFall action)
    {
        return this.Action == action.Action;
    }
}
public class DanceOrFallProperty : IJesterProperty
{
    private SDanceOrFall action;
    public IJesterPropertyInfo Info { get => action; set => action = (SDanceOrFall)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SDanceOrFall)
        {
            return action.CompareAction((SDanceOrFall)_info);
        }
        else
        {
            return false;
        }
    }
}
