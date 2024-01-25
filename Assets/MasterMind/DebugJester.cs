using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugJester : MonoBehaviour
{
    // Start is called before the first frame update

    JesterManager JesterMgr;
    void Start()
    {
        JesterMgr = JesterManager.GetInstance();
        if (JesterMgr == null)
        {
            Debug.Log("Unable to create Jester Manager");
            return;
        }

        //Define what property to use for the round
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.Color);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.Action);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.Mask);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.Pompom);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.Voice);

        //Create the combinaison of properties to find according to the allowed ones for the round
        JesterMgr.GenerateCombinaisonToFind();

        //Fake jester for testing
        Jester playerJester = new Jester();

        ColorProperty colorProperty = new ColorProperty();
        IJesterPropertyInfo info = new SColor(EColor.Red);
        colorProperty.Info = info;
        playerJester.AddProperty(colorProperty);

        PompomProperty pompomProperty = new PompomProperty();
        IJesterPropertyInfo infoP = new SNumberOfPompom(2);
        pompomProperty.Info = infoP;
        playerJester.AddProperty(pompomProperty);

        VoiceProperty voiceProperty = new VoiceProperty();
        IJesterPropertyInfo infoV = new SVoice(EVoice.High);
        voiceProperty.Info = infoV;
        playerJester.AddProperty(voiceProperty);

        MaskProperty maskProperty = new MaskProperty();
        IJesterPropertyInfo infoM = new SMask(EMask.Surprised);
        maskProperty.Info = infoM;
        playerJester.AddProperty(maskProperty);

        ActionProperty actionProperty = new ActionProperty();
        IJesterPropertyInfo infoA = new SAction(EAction.Dance);
        actionProperty.Info = infoA;
        playerJester.AddProperty(actionProperty);

        int goodPropertiesFound = JesterMgr.CheckCombinaison(playerJester);

        if(goodPropertiesFound == JesterMgr.authorizedProperties.Count)
        {
            Debug.Log("Found !");
        }
        else
        {
            Debug.Log("Not found... Correct properties : " + goodPropertiesFound + " / " + JesterMgr.authorizedProperties.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
