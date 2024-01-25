using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EFartOrBallKick
{
    None = default,
    Fart,
    BallsKick
}
public struct SFartOrBallKick : IJesterPropertyInfo
{
    public SFartOrBallKick(EFartOrBallKick _action = EFartOrBallKick.None)
    {
        Action = _action;
    }

    public EFartOrBallKick Action;

    public bool CompareAction(SFartOrBallKick action)
    {
        return this.Action == action.Action;
    }
}
public class FartOrBallKickProperty : IJesterProperty
{
    private SFartOrBallKick action;
    public IJesterPropertyInfo Info { get => action; set => action = (SFartOrBallKick)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SFartOrBallKick)
        {
            return action.CompareAction((SFartOrBallKick)_info);
        }
        else
        {
            return false;
        }
    }
}
