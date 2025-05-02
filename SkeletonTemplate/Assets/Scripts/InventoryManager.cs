using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int eggCount = 0;
    public int eggsNeededForKey = 3;
    public int eggsNeededForEnd = 5;
    public DialogueManager dialogueManager;
    public DialogueData keyDialogue;

    private bool dialogueTriggered = false;

    public GameObject keyObjectToEnable; // Assign this in the Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        keyObjectToEnable.SetActive(false);
    }

    public void AddEgg()
    {
        eggCount++;
        Debug.Log("Egg Count: " + eggCount);

        if (eggCount == eggsNeededForKey)
        {
            EnableKey();
            TriggerDialogue();
            
        }
    }

    private void EnableKey()
    {
        if (keyObjectToEnable != null)
        {
            keyObjectToEnable.SetActive(true);
            Debug.Log("Key Enabled!");
        }
        else
        {
            Debug.LogError("No key object assigned to enable!");
        }
    }

    private void TriggerDialogue()
    {
        dialogueManager.StartDialogue(keyDialogue);
    }
}
