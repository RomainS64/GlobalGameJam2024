using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EquipmentManager : MonoSingleton<EquipmentManager>
{
    [SerializeField] private GameObject equipmentCanvas;
    private CameraManager cameraManager;
    private JesterEquipmentHandler linkedJester = null;
    
    [SerializeField] public UISelectable colorSelectable;
    [SerializeField] public UISelectable creamOrRakeSelectable;
    [SerializeField] public UISelectable danceOrFallSelectable;
    [SerializeField] public UISelectable fartOrBallKickSelctable;
    [SerializeField] public UISelectable maskSelectable;
    [SerializeField] public UISelectable pompomSelectable;
    [SerializeField] public UISelectable voiceSelectable;
    

    public void LockAllSelectables()
    {
        colorSelectable.IsLocked = true;
        creamOrRakeSelectable.IsLocked = true;
        danceOrFallSelectable.IsLocked = true;
        fartOrBallKickSelctable.IsLocked = true;
        maskSelectable.IsLocked = true;
        pompomSelectable.IsLocked = true;
        voiceSelectable.IsLocked = true;
    }
    private void Start()
    {
        HideEquipmentUI();
        cameraManager = FindObjectOfType<CameraManager>();
        colorSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SColor((EColor)i));
        };
        creamOrRakeSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SCreamOrRake((ECreamOrRake)i));
        };
        danceOrFallSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SDanceOrFall((EDanceOrFall)i));
        };
        fartOrBallKickSelctable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SFartOrBallKick((EFartOrBallKick)i));
        };
        maskSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SMask((EMask)i));   
        };
        pompomSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SNumberOfPompom(i));
        };
        voiceSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SVoice((EVoice)i));
        };
    }
    public void Validate()
    {
        int combinaison = JesterManager.GetInstance().CheckCombinaison(linkedJester.PlayerJester);
        int max = JesterManager.GetInstance().AuthorizedCount();
        Debug.Log("Found : "+combinaison +" / "+max);
        StartCoroutine(KingValidation(combinaison,max));

    }

    private float charabiaCourtTime = 1f;
    private float charabiaLongTime = 3f;
    private float kingReactionTime = 2f;
    private float endAnimationTime = 2f;
    IEnumerator KingValidation(int value,int max)
    {
        AudioManager.Instance.PauseSong(EAudioSourceType.ENVIRONEMENT);
        //1 AnimBouffon
        bool isCharabiaLong = Random.Range(0,2) == 0;
        AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Charabia"+(isCharabiaLong?"Long":"Court"));
        linkedJester.DoSpectacle();
        HideEquipmentUI();
        yield return new WaitForSeconds(isCharabiaLong?charabiaLongTime:charabiaCourtTime);
        //int val = (int)MathF.Ceiling(Mathf.Lerp(0,7,Mathf.InverseLerp(0, value, max)));
        int val = value * 7 / max;

        string tag = "";
        switch (val)
        {
            case 0:
                tag = "Insatisfait";
                break;
            case 1:
                tag = "Indice1";
                break;
            case 2:
                tag = "Indice2";
                break;
            case 3:
                tag = "Indice3";
                break;
            case 4:
                tag = "Indice4";
                break;
            case 5:
                tag = "Indice5";
                break;
            case 6:
                tag = "Indice6";
                break;
            case 7:
                tag = "Heureux";
                break;
        }

        Debug.Log(val+" : "+tag);
        AudioManager.Instance.PlaySongByTypeAndTag("King", tag);
        KingManager.Instance.KingReaction(val);
        
        yield return new WaitForSeconds(kingReactionTime);
        if (value > GameManager.Instance.ValidationThreshold)
        {
            AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Reussite");
        }
        else
        {
            AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Echec");
        }
        yield return new WaitForSeconds(endAnimationTime);
        cameraManager.ZoomOut();
        yield return new WaitForSeconds(3f);
        
        if (value == max)
        {
            GameManager.Instance.FinishRound(true);
        }

        GameManager.Instance.IsInJesterSelection = true;
        if (value > GameManager.Instance.ValidationThreshold)
        {
            linkedJester.ResetPosition();
        }
        else
        {
            if (FindObjectsOfType<JesterEquipmentHandler>().Length == 1)
            {
                GameManager.Instance.FinishRound(false);
            }
            linkedJester.Die();
        }
        AudioManager.Instance.ResumeSong(EAudioSourceType.ENVIRONEMENT);
    }
    public void DisplayEquipmentUI(JesterEquipmentHandler jester)
    {
        linkedJester = jester;
        equipmentCanvas.SetActive(true);   
        SetupUIWithJester(jester);
    }

    public void SetupUIWithJester(JesterEquipmentHandler jester)
    {
        colorSelectable.SetCurrent((int)((SColor)jester.colorProperty.Info).Color);
        danceOrFallSelectable.SetCurrent((int)((SDanceOrFall)jester.danceProperty.Info).Action);
        fartOrBallKickSelctable.SetCurrent((int)((SFartOrBallKick)jester.fartProperty.Info).Action);
        creamOrRakeSelectable.SetCurrent((int)((SCreamOrRake)jester.rakeProperty.Info).Action);
        maskSelectable.SetCurrent((int)((SMask)jester.maskProperty.Info).Mask);
        pompomSelectable.SetCurrent(((SNumberOfPompom)jester.pompomProperty.Info).NbPompom);
        voiceSelectable.SetCurrent((int)((SVoice)jester.voiceProperty.Info).Voice);
    }

    public void HideEquipmentUI()
    {
        equipmentCanvas.SetActive(false);   
    }
}
