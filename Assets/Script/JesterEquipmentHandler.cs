using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JesterEquipmentHandler : MonoBehaviour
{
    public Animator jesterEquipmentAnimator;
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
        IJesterPropertyInfo info = new SColor((EColor)Random.Range(0,5));
        colorProperty.Info = info;
        PlayerJester.AddProperty(colorProperty);


        IJesterPropertyInfo infoP = new SNumberOfPompom(Random.Range(0,5));
        pompomProperty.Info = infoP;
        PlayerJester.AddProperty(pompomProperty);
        
        IJesterPropertyInfo infoV = new SVoice((EVoice)Random.Range(0,3));
        voiceProperty.Info = infoV;
        PlayerJester.AddProperty(voiceProperty);
        
        IJesterPropertyInfo infoM = new SMask((EMask)Random.Range(0,5));
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
        //TODO, walk to pos
        transform.position = spawner.GetRandomPoint();
    }

    public void DoSpectacle()
    {
        
    }

    public void Die()
    {
        //TODO: die anim
        Destroy(gameObject);
    }

    public void Rotate()
    {
        StartCoroutine(RotateCoroutine(0.5f));
    }

    public IEnumerator RotateCoroutine(float time)
    {
        Vector3 baseRotation = transform.rotation.eulerAngles;
        for (int i = 0; i < 50; i++)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 180f / 50f, 0));
            yield return new WaitForSeconds(time /50f);
        }
        transform.rotation = Quaternion.Euler(baseRotation + new Vector3(0, 180, 0));
    }
}
