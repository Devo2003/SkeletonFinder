using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public string animationTriggerName = "UnlockGate"; //Name of trigger in animator
    
    public Animator animator;
    public GameObject item;
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            animator.SetTrigger(animationTriggerName);
        }
    }



}
