using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ScriptableObject to store dialogue lines
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] dialogueLines; // Array of dialogue lines
}

[System.Serializable]
public class DialogueLine
{
    public string text;
    public Sprite characterImage;
}



// Abstract class for dialogue triggers
public abstract class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData; // Holds the dialogue data to be triggered
    private bool hasPlayed = false; //Flag to prevent replaying

    protected void TriggerDialogue()
    {
        if (dialogueData != null && !hasPlayed)
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
            hasPlayed = true; //Mark as played
        }
    }
}
