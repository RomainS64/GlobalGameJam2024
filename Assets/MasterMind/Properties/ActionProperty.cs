using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EAction
{
    Fart,
    BallsKick,
    Dance,
    Fall,
    CreamPie,
    Rake
}
public struct SAction : IJesterPropertyInfo
{
    public SAction(EAction _action)
    {
        Action = _action;
    }

    public EAction Action;

    public bool CompareAction(SAction action)
    {
        return this.Action == action.Action;
    }
}
public class ActionProperty : IJesterProperty
{
    private SAction action;
    public IJesterPropertyInfo Info { get => action; set => action = (SAction)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SAction)
        {
            return action.CompareAction((SAction)_info);
        }
        else
        {
            return false;
        }
    }
}
