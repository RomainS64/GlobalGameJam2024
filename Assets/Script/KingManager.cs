using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingManager : MonoSingleton<KingManager>
{
    [SerializeField] private Animator animator;

    public void KingReaction(int goodness,int maxGoodness)
    {
        animator.SetInteger("State",goodness);
    }
}
