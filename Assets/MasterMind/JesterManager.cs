using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAuthorizedProperty
{
    Color,
    Pompom,
    Voice,
    Mask,
    Action
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

                case EAuthorizedProperty.Action:
                    ActionProperty actionProperty = new ActionProperty();
                    IJesterPropertyInfo infoA = new SAction(EAction.Dance);
                    actionProperty.Info = infoA;
                    JesterToFind.AddProperty(actionProperty);
                    break;
            }
            
        }
    }

    public int CheckCombinaison(Jester jester)
    {
        return JesterToFind.CompareJester(jester);
    }
}
