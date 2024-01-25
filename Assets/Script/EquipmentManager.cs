using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoSingleton<EquipmentManager>
{
    [SerializeField] private GameObject equipmentCanvas;
    private CameraManager cameraManager;
    private JesterEquipmentHandler linkedJester = null;
    private void Start()
    {
        HideEquipmentUI();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            DisplayEquipmentUI(null);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            HideEquipmentUI();            
        }
    }

    public void Validate()
    {
        StartCoroutine(KingValidation(true));
    }

    IEnumerator KingValidation(bool isValid)
    {
        HideEquipmentUI();
        cameraManager.ZoomOut();
        linkedJester.ResetPosition();
        yield return new WaitForSeconds(3f);
        
        GameManager.Instance.IsInJesterSelection = true;
    }
    public void DisplayEquipmentUI(JesterEquipmentHandler jester)
    {
        linkedJester = jester;
        equipmentCanvas.SetActive(true);   
    }

    public void HideEquipmentUI()
    {
        equipmentCanvas.SetActive(false);   
    }
}
