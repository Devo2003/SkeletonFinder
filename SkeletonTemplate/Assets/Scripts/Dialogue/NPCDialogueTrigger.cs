using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : DialogueTrigger
{
    public KeyCode interactKey = KeyCode.E; // Key used for interaction
    private bool playerInRange = false; // Flag to track if player is in range

    private void Update()
    {
        // Check if player is in range and presses the interaction key
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
