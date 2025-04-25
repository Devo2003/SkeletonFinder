using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : DialogueTrigger
{
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;
    public DialogueData defaultDialogue; // Before getting the key
    public DialogueData keyDialogue; // After getting the key

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerCollect.hasKey) // Player has collected the key
            {
                dialogueData = keyDialogue; // Use updated dialogue
                DialogueManager.Instance.StartDialogue(dialogueData, true); // End game after dialogue
            }
            else
            {
                dialogueData = defaultDialogue;
                TriggerDialogue();
            }
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
