using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public string[] dialogueLines; // Dialogue text array
    private bool playerInRange = false; // Check if player is in range

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player enters the trigger
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player leaves the trigger
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // Press "E" to start dialogue
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueLines);
        }
    }
}
