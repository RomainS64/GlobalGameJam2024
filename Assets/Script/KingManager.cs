using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingManager : MonoSingleton<KingManager>
{
    [SerializeField] private Animator animator;

    public void KingReaction(int goodness,int maxGoodness)
    {
        float inverseLerp = Mathf.InverseLerp(0, maxGoodness, goodness);
        
        animator.SetInteger("State",(int)Mathf.Lerp(0,7,inverseLerp));
    }
}
