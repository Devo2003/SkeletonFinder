using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // TMP text component
    public GameObject dialoguePanel; // The UI panel for dialogue
    public float typingSpeed = 0.05f; // Speed of the typing effect

    private string[] currentDialogue; // Array of dialogue lines
    private int currentLine = 0; // Current dialogue line index
    private int currentCharIndex = 0; // Character index for typing effect
    private float timer; // Timer for typing speed
    private bool isTyping = false; // Is the text currently typing?

    private void Start()
    {
        dialoguePanel.SetActive(false); // Hide dialogue panel at the start
    }

    public void StartDialogue(string[] dialogue)
    {
        if (dialogue == null || dialogue.Length == 0)
        {
            Debug.LogError("StartDialogue() received a null or empty dialogue array!");
            return;
        }

        Debug.Log("StartDialogue() was called! Starting dialogue...");

        dialoguePanel.SetActive(true);
        currentDialogue = dialogue;
        currentLine = 0;
        currentCharIndex = 0;
        dialogueText.text = "";
        isTyping = true;
    }

    private void Update()
    {
        if (isTyping)
        {
            timer += Time.deltaTime;

            if (timer >= typingSpeed)
            {
                timer = 0f;

                if (currentCharIndex < currentDialogue[currentLine].Length)
                {
                    dialogueText.text = currentDialogue[currentLine].Substring(0, currentCharIndex + 1);
                    currentCharIndex++;
                }
                else
                {
                    isTyping = false; // Typing finished
                    Debug.Log("Typing completed for this line.");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E)) // Move to the next line
        {
            Debug.Log("E pressed, calling ShowNextLine()");
            ShowNextLine();
        }
    }

    public void ShowNextLine()
    {
        Debug.Log($"Trying to show next line... Current line: {currentLine}/{currentDialogue.Length - 1}");

        if (currentLine < currentDialogue.Length - 1) // Check if there's more dialogue
        {
            currentLine++; // Move to the next line
            currentCharIndex = 0; // Reset typing effect
            dialogueText.text = ""; // Clear text before typing
            isTyping = true; // Start typing effect

            Debug.Log($"Now displaying line {currentLine}: {currentDialogue[currentLine]}");
        }
        else
        {
            Debug.Log("End of dialogue reached! Calling EndDialogue().");
            EndDialogue();
        }
    }


    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Hide dialogue panel
    }
}
