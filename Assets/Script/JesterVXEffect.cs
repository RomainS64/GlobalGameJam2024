using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class JesterVXEffect : MonoBehaviour
{
    [SerializeField] private GameObject fartVX;
    [SerializeField] private Transform fartVXSpawnPosition;

    [SerializeField] private GameObject fallToGroundVX;
    [SerializeField] private Transform fallToGroundVXSpawnPosition;

    public void OnFart()
    {
        Instantiate(fartVX, fartVXSpawnPosition.position, Quaternion.identity);
        AudioManager.Instance.PlaySongByTypeAndTag("other", "leprout", EAudioSourceType.SFX_OTHER);
    }

    public void OnLunchPie()
    {

    }

    public void OnFallToGround()
    {
        Instantiate(fallToGroundVX, fallToGroundVXSpawnPosition.position, Quaternion.identity);
    }

    public void OnStartedDance()
    {
        AudioManager.Instance.PlaySongByTypeAndTag("other", "FortshitDance", EAudioSourceType.SFX_OTHER);
    }
}
