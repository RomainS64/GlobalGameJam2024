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
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.DanceOrFall);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.CreamOrRake);
        JesterMgr.AuthorizeProperty(EAuthorizedProperty.FartOrBallsKick);
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

        FartOrBallKickProperty fartProperty = new FartOrBallKickProperty();
        IJesterPropertyInfo infoF = new SFartOrBallKick(EFartOrBallKick.Fart);
        fartProperty.Info = infoF;
        playerJester.AddProperty(fartProperty);

        DanceOrFallProperty danceProperty = new DanceOrFallProperty();
        IJesterPropertyInfo infoD = new SDanceOrFall(EDanceOrFall.Fall);
        danceProperty.Info = infoD;
        playerJester.AddProperty(danceProperty);

        CreamOrRakeProperty rakeProperty = new CreamOrRakeProperty();
        IJesterPropertyInfo infoR = new SCreamOrRake(ECreamOrRake.CreamPie);
        rakeProperty.Info = infoR;
        playerJester.AddProperty(rakeProperty);

        MaskProperty maskProperty2 = new MaskProperty();
        IJesterPropertyInfo infoM2 = new SMask(EMask.Happy);
        maskProperty2.Info = infoM2;
        playerJester.AddProperty(maskProperty2);

        int goodPropertiesFound = JesterMgr.CheckCombinaison(playerJester);

        if(goodPropertiesFound == JesterMgr.authorizedProperties.Count)
        {
            Debug.Log("Found !");
        }
        else
        {
            Debug.Log("Not found... Correct properties : " + goodPropertiesFound + " / " + JesterMgr.authorizedProperties.Count);
        }

        infoM = new SMask(EMask.Lover);
        playerJester.SetProperty(infoM);

        goodPropertiesFound = JesterMgr.CheckCombinaison(playerJester);

        if (goodPropertiesFound == JesterMgr.authorizedProperties.Count)
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
