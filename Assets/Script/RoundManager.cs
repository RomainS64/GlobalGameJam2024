using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SRound
{
    public EAuthorizedProperty[] authorizedProperties;
    public int numberOfJesters;
    public int validationThreshold;
}
public class RoundManager : MonoSingleton<RoundManager>
{
    private int currentRound = 1;
    public int currentTentative = 0;
    private int maxTentative;
    public SRound[] rounds;

    private JesterManager jesterMgr;
    private EquipmentManager equipmentManager;

    public void StartRound()
    {
        
        if(rounds.Count() < currentRound) 
        { 
            return; 
        }
        maxTentative = rounds[currentRound - 1].numberOfJesters;
        GameManager.Instance.ValidationThreshold = rounds[currentRound - 1].validationThreshold;
        
        jesterMgr = JesterManager.GetInstance();
        if (jesterMgr == null)
        {
            Debug.Log("Unable to create Jester Manager");
            return;
        }

        equipmentManager = EquipmentManager.Instance;
        if (equipmentManager == null)
        {
            Debug.Log("Unable to create Equipement Manager");
            return;
        }

        jesterMgr.ResetAuthorizedProperties();
        equipmentManager.LockAllSelectables();
        equipmentManager.kingHumor = KingHumor.Happy;
        equipmentManager.textHumor.SetText("The King is happy");
        GameManager.Instance.ValidationThreshold = 0;

        //Define what property to use for the round
        foreach (EAuthorizedProperty property in rounds[currentRound - 1].authorizedProperties)
        {
            switch (property)
            {
                case EAuthorizedProperty.Color:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.Color);
                    equipmentManager.colorSelectable.IsLocked = false;
                    break;

                case EAuthorizedProperty.Pompom:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.Pompom);
                    equipmentManager.pompomSelectable.IsLocked = false;
                    break;

                case EAuthorizedProperty.Voice:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.Voice);
                    equipmentManager.voiceSelectable.IsLocked = false;
                    break;

                case EAuthorizedProperty.Mask:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.Mask);
                    equipmentManager.maskSelectable.IsLocked = false;
                    break;

                case EAuthorizedProperty.FartOrBallsKick:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.FartOrBallsKick);
                    equipmentManager.fartOrBallKickSelctable.IsLocked = false;
                    break;

                case EAuthorizedProperty.DanceOrFall:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.DanceOrFall);
                    equipmentManager.danceOrFallSelectable.IsLocked = false;
                    break;

                case EAuthorizedProperty.CreamOrRake:
                    jesterMgr.AuthorizeProperty(EAuthorizedProperty.CreamOrRake);
                    equipmentManager.creamOrRakeSelectable.IsLocked = false;
                    break;
            }
        }

        jesterMgr.GenerateCombinaisonToFind();

        JesterSpawner jesterSpawner = JesterSpawner.Instance;
        jesterSpawner.DeleteAllJester();
        jesterSpawner.SpawnJester(maxTentative);
    }

    public void GoNextRound()
    {
        if(currentRound != rounds.Length)currentRound++;
        StartRound();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)) 
        {
            GoNextRound();
        }
    }
}
