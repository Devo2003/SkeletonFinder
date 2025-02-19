using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private TextMeshProUGUI dialogueText; // Reference to the UI text component
    [SerializeField] private GameObject dialogueUI; // Reference to the UI panel for dialogue

    private Queue<string> dialogueQueue = new Queue<string>(); // Queue to store dialogue lines
    private bool isDialogueActive = false; // Flag to check if dialogue is active

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("DialogueManager instance set.");
        }
        else
        {
            Debug.LogError("Multiple DialogueManager instances found! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    // Starts a new dialogue sequence
    public void StartDialogue(DialogueData dialogue)
    {
        if (isDialogueActive) return;


        if (dialogueUI == null)
        {
            Debug.LogError("DialogueManager: dialogueUI is NOT assigned in the Inspector.");
            return;
        }
        if (dialogueText == null)
        {
            Debug.LogError("DialogueManager: dialogueText is NOT assigned in the Inspector.");
            return;
        }

        isDialogueActive = true;
        dialogueUI.SetActive(true);
        dialogueQueue.Clear();

        // Enqueue each dialogue line
        foreach (string line in dialogue.dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine(); // Display the first line
    }

    // Displays the next line of dialogue
    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue(); // End dialogue if no more lines remain
            return;
        }

        dialogueText.text = dialogueQueue.Dequeue(); // Display the next line
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    // Ends the dialogue sequence
    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
    }
}
