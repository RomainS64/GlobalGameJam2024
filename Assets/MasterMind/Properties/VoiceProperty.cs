using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EVoice
{
    High,
    Neutral,
    Deep
}
public struct SVoice : IJesterPropertyInfo
{
    public SVoice(EVoice _voice)
    {
        Voice = _voice;
    }

    public EVoice Voice;

    public bool CompareVoice(SVoice voice)
    {
        return this.Voice == voice.Voice;
    }
}
public class VoiceProperty : IJesterProperty
{
    private SVoice voice;
    public IJesterPropertyInfo Info { get => voice; set => voice = (SVoice)value; }
    public bool CompareProperty(IJesterPropertyInfo _info)
    {
        if (_info is SVoice)
        {
            return voice.CompareVoice((SVoice)_info);
        }
        else
        {
            return false;
        }
    }
}
