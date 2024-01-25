using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JesterEquipmentHandler : MonoBehaviour
{
    
    private JesterSpawner spawner;
    public Jester PlayerJester { get; private set; }
    public ColorProperty colorProperty = new ColorProperty();
    public  PompomProperty pompomProperty = new PompomProperty();
    public VoiceProperty voiceProperty = new VoiceProperty();
    public MaskProperty maskProperty = new MaskProperty();
    public FartOrBallKickProperty fartProperty = new FartOrBallKickProperty();
    public DanceOrFallProperty danceProperty = new DanceOrFallProperty();
    public CreamOrRakeProperty rakeProperty = new CreamOrRakeProperty();
    private void Start()
    {
        spawner = FindObjectOfType<JesterSpawner>();
        PlayerJester = new Jester();
        IJesterPropertyInfo info = new SColor();
        colorProperty.Info = info;
        PlayerJester.AddProperty(colorProperty);


        IJesterPropertyInfo infoP = new SNumberOfPompom();
        pompomProperty.Info = infoP;
        PlayerJester.AddProperty(pompomProperty);
        
        IJesterPropertyInfo infoV = new SVoice((EVoice)Random.Range(0,3));
        voiceProperty.Info = infoV;
        PlayerJester.AddProperty(voiceProperty);
        
        IJesterPropertyInfo infoM = new SMask();
        maskProperty.Info = infoM;
        PlayerJester.AddProperty(maskProperty);
        
        IJesterPropertyInfo infoF = new SFartOrBallKick();
        fartProperty.Info = infoF;
        PlayerJester.AddProperty(fartProperty);

        IJesterPropertyInfo infoD = new SDanceOrFall();
        danceProperty.Info = infoD;
        PlayerJester.AddProperty(danceProperty);
        
        IJesterPropertyInfo infoR = new SCreamOrRake();
        rakeProperty.Info = infoR;
        PlayerJester.AddProperty(rakeProperty);
    }

    public void ResetPosition()
    {
        transform.position = spawner.GetRandomPoint();
    }
}
