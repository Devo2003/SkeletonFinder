using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Image characterImage; // Add UI Image to show the character's image

    private Queue<DialogueLine> dialogueQueue = new Queue<DialogueLine>(); // Queue to store dialogue lines with character info
    public bool isDialogueActive = false;
    private bool isDialogueEnabled = true;
    public bool isEndingDialogue = false; // Check if it's the last dialogue

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        characterImage.enabled = false; //disables character sprite at runtime
    }

    private bool isInputPaused = false;

    public void PauseDialogueInput(bool pause)
    {
        isInputPaused = pause;
    }

    public void EnableDialogue(bool enable)
    {
        isDialogueEnabled = enable;
        if (!enable && isDialogueActive)
        {
            EndDialogue();
        }
    }

    public void StartDialogue(DialogueData dialogue, bool isEndGameDialogue = false)
    {
        if (isDialogueActive || !isDialogueEnabled) return;

        isEndingDialogue = isEndGameDialogue;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        characterImage.enabled = true;
        dialogueText.text = "";
        dialogueQueue.Clear();

        // Queue each line of dialogue along with the character's associated image
        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        dialogueText.text = currentLine.text;

        // Set the character image for the current line
        characterImage.sprite = currentLine.characterImage; // Assuming each DialogueLine has a character image
    }

    private void Update()
    {
        if (isDialogueActive && !isInputPaused && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueText.text = "";
        dialogueUI.SetActive(false);
        characterImage.enabled = false;

        if (isEndingDialogue)
        {
            Debug.Log("Ending game...");
            //EndGame();
        }
    }

  
}
