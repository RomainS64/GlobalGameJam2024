using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ECreamOrRake
{
    CreamPie,
    Rake
}
public struct SCreamOrRake : IJesterPropertyInfo
{
    public SCreamOrRake(ECreamOrRake _action)
    {
        Action = _action;
    }

    public ECreamOrRake Action;

    public bool CompareAction(SCreamOrRake action)
    {
        return this.Action == action.Action;
    }
}
public class CreamOrRakeProperty : IJesterProperty
{
    private SCreamOrRake action;
    public IJesterPropertyInfo Info { get => action; set => action = (SCreamOrRake)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SCreamOrRake)
        {
            return action.CompareAction((SCreamOrRake)_info);
        }
        else
        {
            return false;
        }
    }
}
