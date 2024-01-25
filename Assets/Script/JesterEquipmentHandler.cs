using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterEquipmentHandler : MonoBehaviour
{
    private JesterSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<JesterSpawner>();
    }

    public void ResetPosition()
    {
        transform.position = spawner.GetRandomPoint();
    }
}
