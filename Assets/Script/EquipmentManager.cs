using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum KingHumor
{
    Happy,
    Bored,
    Angry
}

public struct SFoundInLastRounds
{
    public int nbFound;
    public bool isNew;

    public SFoundInLastRounds(int _nbFound = 0)
    {
        nbFound = _nbFound;
        isNew = false;
    }

    public void OnFind()
    {
        if (nbFound < 3)
        {
            nbFound++;
        }

        if (nbFound == 1)
        {
            isNew = true;
        }

    }

    public bool Compare(SFoundInLastRounds other)
    {
        return this.nbFound == other.nbFound;
    }
}

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
    [SerializeField] public TMP_Text textHumor;

    public Dictionary<string, SFoundInLastRounds> propertiesFoundInRound;
    public KingHumor kingHumor;
    

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
        propertiesFoundInRound = new Dictionary<string, SFoundInLastRounds>();
        HideEquipmentUI();
        cameraManager = FindObjectOfType<CameraManager>();
        colorSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SColor((EColor)i));
        };
            propertiesFoundInRound.Add(nameof(SColor), new SFoundInLastRounds());
        creamOrRakeSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SCreamOrRake((ECreamOrRake)i));
        };
            propertiesFoundInRound.Add(nameof(SCreamOrRake), new SFoundInLastRounds());
        danceOrFallSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SDanceOrFall((EDanceOrFall)i));
        };
            propertiesFoundInRound.Add(nameof(SDanceOrFall), new SFoundInLastRounds());
        fartOrBallKickSelctable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SFartOrBallKick((EFartOrBallKick)i));
        };
            propertiesFoundInRound.Add(nameof(SFartOrBallKick), new SFoundInLastRounds());
        maskSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SMask((EMask)i));
        };
            propertiesFoundInRound.Add(nameof(SMask), new SFoundInLastRounds());
        pompomSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SNumberOfPompom(i));
        };
            propertiesFoundInRound.Add(nameof(SNumberOfPompom), new SFoundInLastRounds());
        voiceSelectable.OnModified += (int i) =>
        {
            linkedJester.PlayerJester.SetProperty(new SVoice((EVoice)i));
        };
            propertiesFoundInRound.Add(nameof(SVoice), new SFoundInLastRounds());
    }
    public void ResetLastRoundsData()
    {
        propertiesFoundInRound.Clear();
        propertiesFoundInRound.Add(nameof(SColor), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SCreamOrRake), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SDanceOrFall), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SFartOrBallKick), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SMask), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SNumberOfPompom), new SFoundInLastRounds());
        propertiesFoundInRound.Add(nameof(SVoice), new SFoundInLastRounds());
        RoundManager.Instance.currentTentative = 0;
    }

    public void OnNewPropertyFound()
    {
        Dictionary<string, SFoundInLastRounds> tempPropertiesFoundInRound = new Dictionary<string, SFoundInLastRounds>(propertiesFoundInRound);
        ResetLastRoundsData();
        foreach (var item in tempPropertiesFoundInRound)
        {
            if (item.Value.nbFound > 0)
            {
                if (propertiesFoundInRound.TryGetValue(item.Key, out SFoundInLastRounds foundInRound))
                {
                    foundInRound.nbFound = 1;
                    propertiesFoundInRound[item.Key] = foundInRound;
                }
            }
        }
    }

    public bool NoNewPropertyFoundAfterRounds(Dictionary<string, SFoundInLastRounds> lastRound)
    {
        bool propertyFoundInAllLastRounds = false;
        bool oneDifferent = false;
        foreach (var item in lastRound)
        {
            SFoundInLastRounds foundInRound;
            propertiesFoundInRound.TryGetValue(item.Key, out foundInRound);

            if (foundInRound.nbFound == 3)
            {
                propertyFoundInAllLastRounds = true;
            }
            else
            {
                if (!foundInRound.Compare(item.Value))
                {
                    oneDifferent = true;
                }
            }
        }
        return propertyFoundInAllLastRounds && (oneDifferent || RoundManager.Instance.currentTentative == 3) ;
    }

    public void Validate()
    {
        Dictionary<string, SFoundInLastRounds> propertiesFoundInPreviousRound = new Dictionary<string, SFoundInLastRounds>(propertiesFoundInRound);
        int combinaison = JesterManager.GetInstance().CheckCombinaison(linkedJester.PlayerJester);
        RoundManager.Instance.currentTentative++;
        int max = JesterManager.GetInstance().AuthorizedCount();
        Debug.Log("Found : "+combinaison +" / "+max);
        StartCoroutine(KingValidation(combinaison,max));

        if (!(kingHumor == KingHumor.Angry))
        {
            if (NoNewPropertyFoundAfterRounds(propertiesFoundInPreviousRound))
            {
                switch (kingHumor)
                {
                    case KingHumor.Happy:
                        kingHumor = KingHumor.Bored;
                        textHumor.SetText("The King is bored");
                        GameManager.Instance.ValidationThreshold = max / 2;
                        break;
                    case KingHumor.Bored:
                        kingHumor = KingHumor.Angry;
                        textHumor.SetText("The King is angry");
                        GameManager.Instance.ValidationThreshold = max;
                        break;
                }
                ResetLastRoundsData();
            }
        }

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
        AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Charabia"+(isCharabiaLong?"Long":"Court"),EAudioSourceType.SFX_JESTER);
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
        AudioManager.Instance.PlaySongByTypeAndTag("King", tag,EAudioSourceType.SFX_KING);
        KingManager.Instance.KingReaction(val);
        
        yield return new WaitForSeconds(kingReactionTime);
        if (value > GameManager.Instance.ValidationThreshold)
        {
            AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Reussite",EAudioSourceType.SFX_JESTER);
        }
        else
        {
            AudioManager.Instance.PlaySongByTypeAndTag(((SVoice)linkedJester.voiceProperty.Info).Voice.ToString(),"Echec", EAudioSourceType.SFX_JESTER);
        }
        yield return new WaitForSeconds(endAnimationTime);
        cameraManager.ZoomOut();
        yield return new WaitForSeconds(3f);
        
        if (value == max)
        {
            GameManager.Instance.FinishRound(true);
            ResetLastRoundsData();
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
