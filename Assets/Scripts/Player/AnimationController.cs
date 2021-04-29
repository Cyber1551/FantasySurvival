using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public delegate void AnimationEvent();
    public AnimationEvent BasicAttackDelegate;
   void Awake()
    {
        animator = GetComponent<Animator>();
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    public bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    public void SetSpeed(float speed)
    {
      animator.speed = speed;
      
    }

    public void UpdateAnimator(float forwardAmount, float turnAmount)
    {
       animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
       animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
    }


    //Animation Events 
    private void BasicAttackEvent()
    {
        Debug.Log("BA");
        BasicAttackEvent();
    }
}
