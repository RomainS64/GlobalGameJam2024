using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private GameObject equipmentCanvas;

    private void Start()
    {
        HideEquipmentUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            DisplayEquipmentUI();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            HideEquipmentUI();            
        }
    }

    public void DisplayEquipmentUI()
    {
        equipmentCanvas.SetActive(true);   
    }

    public void HideEquipmentUI()
    {
        equipmentCanvas.SetActive(false);   
    }
}
