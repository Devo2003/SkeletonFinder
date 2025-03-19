using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingTrigger : MonoBehaviour
{
    [Header("UI Settings")]
    public CanvasGroup fadeCanvas; // CanvasGroup for fade effect
    public GameObject textbox; // Textbox object
    public Button actionButton; // Button object
    public float fadeDuration = 1f;
    public float delayBeforeUI = 1.5f;

    private bool isTriggered = false;
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                DisablePlayerControls();
            }

            DisableAllUI(); // ✅ Disable other UI
            StartCoroutine(FadeOutAndShowUI());
        }
    }

    private void DisablePlayerControls()
    {
        if (player != null)
        {
            // Stop player movement and rotation
            player.enabled = false;
            player.movementSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stop FMOD sound
        }
    }

    private IEnumerator FadeOutAndShowUI()
    {
        // ✅ Enable raycasts during fade (necessary for button interaction)
        fadeCanvas.blocksRaycasts = true;

        // ✅ Fade out screen
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1f;

        // ✅ Delay before showing UI
        yield return new WaitForSeconds(delayBeforeUI);

        // ✅ Enable textbox and button
        textbox.SetActive(true);
        actionButton.gameObject.SetActive(true);

        // ✅ Enable interactivity AFTER fade finishes
        fadeCanvas.interactable = true;
        fadeCanvas.blocksRaycasts = true;

    }

    private void DisableAllUI()
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>(true); // Include inactive canvases
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas != fadeCanvas.GetComponent<Canvas>()) // Keep fadeCanvas active
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}