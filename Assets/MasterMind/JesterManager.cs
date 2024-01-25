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
                    IJesterPropertyInfo infoC = new SColor(EColor.Red);
                    colorProperty.Info = infoC;
                    JesterToFind.AddProperty(colorProperty);
                    break;

                case EAuthorizedProperty.Pompom:
                    PompomProperty pompomProperty = new PompomProperty();
                    IJesterPropertyInfo infoP = new SNumberOfPompom(2);
                    pompomProperty.Info = infoP;
                    JesterToFind.AddProperty(pompomProperty);
                    break;

                case EAuthorizedProperty.Voice:
                    VoiceProperty voiceProperty = new VoiceProperty();
                    IJesterPropertyInfo infoV = new SVoice(EVoice.Neutral);
                    voiceProperty.Info = infoV;
                    JesterToFind.AddProperty(voiceProperty);
                    break;

                case EAuthorizedProperty.Mask:
                    MaskProperty maskProperty = new MaskProperty();
                    IJesterPropertyInfo infoM = new SMask(EMask.Surprised);
                    maskProperty.Info = infoM;
                    JesterToFind.AddProperty(maskProperty);
                    break;

                case EAuthorizedProperty.FartOrBallsKick:
                    FartOrBallKickProperty fartProperty = new FartOrBallKickProperty();
                    IJesterPropertyInfo infoF = new SFartOrBallKick(EFartOrBallKick.Fart);
                    fartProperty.Info = infoF;
                    JesterToFind.AddProperty(fartProperty);
                    break;

                case EAuthorizedProperty.DanceOrFall:
                    DanceOrFallProperty danceProperty = new DanceOrFallProperty();
                    IJesterPropertyInfo infoD = new SDanceOrFall(EDanceOrFall.Fall);
                    danceProperty.Info = infoD;
                    JesterToFind.AddProperty(danceProperty);
                    break;

                case EAuthorizedProperty.CreamOrRake:
                    CreamOrRakeProperty rakeProperty = new CreamOrRakeProperty();
                    IJesterPropertyInfo infoR = new SCreamOrRake(ECreamOrRake.CreamPie);
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
