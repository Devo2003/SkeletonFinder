using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueUI;
    private Queue<string> dialogueQueue = new Queue<string>();
    private bool isDialogueActive = false;
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
    }

    public void StartDialogue(DialogueData dialogue, bool isEndGameDialogue = false)
    {
        if (isDialogueActive) return;

        isEndingDialogue = isEndGameDialogue;
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        dialogueText.text = "";
        dialogueQueue.Clear();

        foreach (string line in dialogue.dialogueLines)
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

        dialogueText.text = dialogueQueue.Dequeue();
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueText.text = "";
        dialogueUI.SetActive(false);

        if (isEndingDialogue)
        {
            Debug.Log("Ending game...");
            EndGame();
        }
    }

    private void EndGame()
    {

        // Quit the game or load an end scene
        Application.Quit(); // Closes the game
        // SceneManager.LoadScene("EndScene"); // Uncomment if you have an end scene
    }
}
