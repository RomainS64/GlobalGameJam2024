using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingManager : MonoSingleton<KingManager>
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animator kingAnimator;

    public void KingReaction(int goodness)
    {
        animator.SetInteger("State",goodness);
        if(goodness == 0)
        {
            kingAnimator.SetTrigger("Unsatisfied");
        }
        else
        {
            if(goodness > 0 && goodness < 7) {
                kingAnimator.SetTrigger("Interested");
            }
            else
            {
                if (goodness == 7)
                {
                    kingAnimator.SetTrigger("Laughing");
                }
            }
        }
    }
}
