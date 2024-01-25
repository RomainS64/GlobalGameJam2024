using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class JesterSpawner : MonoSingleton<JesterSpawner>
{
    [SerializeField] private bool forceMobile;
    [SerializeField] private bool forceDesktop;
    [SerializeField] private GameObject jesterPrefab;
    [SerializeField] private Transform[] mobileSpawners;
    [SerializeField] private Transform[] desktopSpawners;
    [SerializeField] private float dispersionMax;

    private void Start()
    {
        
    }

    public void SpawnJester(int jesterCount)
    {
        for (int i = 0; i < jesterCount; i++)
        {
            Instantiate(jesterPrefab, GetRandomPoint(), Quaternion.identity);
        }
    }

    public void DeleteAllJester()
    {
        foreach (JesterEquipmentHandler jester in FindObjectsOfType<JesterEquipmentHandler>())
        {
            Destroy(jester.gameObject);
        }
    }

    public Vector3 GetRandomPoint()
    {
        float dispersionX = Random.Range(-dispersionMax, dispersionMax);
        float dispersionZ = Random.Range(-dispersionMax, dispersionMax);
        int selectedSpawner;
        Transform[] spawners;
        if (forceDesktop)
        {
            selectedSpawner = Random.Range(0, desktopSpawners.Length);
            spawners = desktopSpawners;
        }
        else if (forceMobile)
        {
            selectedSpawner = Random.Range(0, mobileSpawners.Length);
            spawners = mobileSpawners;
        }
        else
        {
#if UNITY_ANDROID || UNITY_IOS
            selectedSpawner = Random.Range(0, mobileSpawners.Length);
            spawners = mobileSpawners;
#else
        selectedSpawner = Random.Range(0, desktopSpawners.Length);
        spawners = desktopSpawners;
#endif
        }

        return spawners[selectedSpawner].position + new Vector3(dispersionX, 0, dispersionZ);
    }
}
