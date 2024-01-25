using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        StartCoroutine(KingValidation(JesterManager.GetInstance().CheckCombinaison(linkedJester.PlayerJester),JesterManager.GetInstance().AuthorizedCount()));
    }

    IEnumerator KingValidation(int value,int max)
    {
        HideEquipmentUI();
        cameraManager.ZoomOut();
        linkedJester.ResetPosition();
        KingManager.Instance.KingReaction(value,max);
        yield return new WaitForSeconds(3f);
        
        GameManager.Instance.IsInJesterSelection = true;
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
