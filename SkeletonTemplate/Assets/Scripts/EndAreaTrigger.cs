using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAreaTrigger : DialogueTrigger
{
    private bool hasTriggered = false;
    [SerializeField] private CanvasGroup fadePanel;
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            // Check if player has all eggs
            if (InventoryManager.Instance.eggCount >= InventoryManager.Instance.eggsNeededForEnd)
            {
                hasTriggered = true;

                // Trigger ending dialogue
                DialogueManager.Instance.StartDialogue(InventoryManager.Instance.endGameDialogue, true);
               

                // Optional: Start a fade out after the dialogue finishes
                StartCoroutine(WaitForDialogueThenFade());
                //EndGame();
            }
        }
    }

    private IEnumerator WaitForDialogueThenFade()
    {
        // Wait until dialogue is done
        while (DialogueManager.Instance.isDialogueActive)
        {
            yield return null;
        }

        // Then start your fade out
        yield return new WaitForSeconds(1f); // Optional delay
        StartCoroutine(FadeOut());
        EndGame();
    }

    private IEnumerator FadeOut()
    {
        // Simple fade-out using a full-screen UI panel (black image with CanvasGroup)
        CanvasGroup fadePanel = GameObject.Find("FadePanel")?.GetComponent<CanvasGroup>();

        if (fadePanel == null)
        {
            Debug.LogWarning("FadePanel not found.");
            yield break;
        }

        float duration = 2f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(0, 1, t / duration);
            yield return null;
        }

        fadePanel.alpha = 1;

        PlayerController.Instance.EnableMovement(false);

        // Reload scene or show credits, etc.
        Debug.Log("Fade complete. Game Over or Restart?");
    }

    public GameObject restartButton; // Assign in Inspector

    private void EndGame()
    {
        Debug.Log("Showing restart button...");
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
        else
        {
            Debug.LogError("Restart button not assigned!");
        }
    }
}
