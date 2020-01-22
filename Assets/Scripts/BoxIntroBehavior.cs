using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIntroBehavior : StateMachineBehaviour
{

    private int rand;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         animator.SetTrigger("Jump");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}