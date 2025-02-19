using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : DialogueTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
