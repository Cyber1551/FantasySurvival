using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehavior_WeaponStance : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().SetWeaponStance(true);
    }
}
