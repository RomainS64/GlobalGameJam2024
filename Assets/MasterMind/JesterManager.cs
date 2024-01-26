using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAuthorizedProperty
{
    Color,
    Pompom,
    Voice,
    Mask,
    FartOrBallsKick,
    DanceOrFall,
    CreamOrRake
}
public class JesterManager
{
    private Jester JesterToFind;
    private static JesterManager instance;
    public List<EAuthorizedProperty> authorizedProperties;
    private JesterManager() 
    {
        authorizedProperties = new List<EAuthorizedProperty>();
    }

    public int AuthorizedCount() => authorizedProperties.Count;
    public void AuthorizeProperty(EAuthorizedProperty property)
    {
        if (!authorizedProperties.Contains(property))
        {
            authorizedProperties.Add(property);
        }
    }

    public void ForbidProperty(EAuthorizedProperty property)
    {
        if(authorizedProperties.Contains(property))
        {
            authorizedProperties.Remove(property);
        }
    }

    public void ResetAuthorizedProperties()
    {
        for (int i = authorizedProperties.Count - 1; i >= 0; i--)
        {
            authorizedProperties.RemoveAt(i);
        }
    }

    public static JesterManager GetInstance()
    {
        if(instance == null)
        {
            instance = new JesterManager();
        }

        return instance;
    }
    public void GenerateCombinaisonToFind()
    {
        JesterToFind = new Jester();

        foreach (EAuthorizedProperty curProperty in authorizedProperties)
        {
            switch (curProperty)
            {
                case EAuthorizedProperty.Color:
                    ColorProperty colorProperty = new ColorProperty();
                    IJesterPropertyInfo infoC = new SColor((EColor)Random.Range(1,5));
                    colorProperty.Info = infoC;
                    JesterToFind.AddProperty(colorProperty);
                    break;

                case EAuthorizedProperty.Pompom:
                    PompomProperty pompomProperty = new PompomProperty();
                    IJesterPropertyInfo infoP = new SNumberOfPompom(Random.Range(1, 5));
                    pompomProperty.Info = infoP;
                    JesterToFind.AddProperty(pompomProperty);
                    break;

                case EAuthorizedProperty.Voice:
                    VoiceProperty voiceProperty = new VoiceProperty();
                    IJesterPropertyInfo infoV = new SVoice((EVoice)Random.Range(0, 2));
                    voiceProperty.Info = infoV;
                    JesterToFind.AddProperty(voiceProperty);
                    break;

                case EAuthorizedProperty.Mask:
                    MaskProperty maskProperty = new MaskProperty();
                    IJesterPropertyInfo infoM = new SMask((EMask)Random.Range(1, 5));
                    maskProperty.Info = infoM;
                    JesterToFind.AddProperty(maskProperty);
                    break;

                case EAuthorizedProperty.FartOrBallsKick:
                    FartOrBallKickProperty fartProperty = new FartOrBallKickProperty();
                    IJesterPropertyInfo infoF = new SFartOrBallKick((EFartOrBallKick)Random.Range(1, 2));
                    fartProperty.Info = infoF;
                    JesterToFind.AddProperty(fartProperty);
                    break;

                case EAuthorizedProperty.DanceOrFall:
                    DanceOrFallProperty danceProperty = new DanceOrFallProperty();
                    IJesterPropertyInfo infoD = new SDanceOrFall((EDanceOrFall)Random.Range(1, 2));
                    danceProperty.Info = infoD;
                    JesterToFind.AddProperty(danceProperty);
                    break;

                case EAuthorizedProperty.CreamOrRake:
                    CreamOrRakeProperty rakeProperty = new CreamOrRakeProperty();
                    IJesterPropertyInfo infoR = new SCreamOrRake((ECreamOrRake)Random.Range(1, 2));
                    rakeProperty.Info = infoR;
                    JesterToFind.AddProperty(rakeProperty);
                    break;
            }
            
        }
    }

    public int CheckCombinaison(Jester jester)
    {
        return JesterToFind.CompareJester(jester);
    }
}
