using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJumpBehavior : StateMachineBehaviour
{
    private float timer;
    public float minTime = 2.5f;
    public float maxTime = 4.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 1)
        {
            FindObjectOfType<CameraShake>().TriggerShake();
        }
        if (timer <= 0)
        {
            animator.SetTrigger("Flip");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
