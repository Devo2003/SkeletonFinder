using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorwayTeleport : MonoBehaviour
{
    public Transform teleportDestination; // Assign in Inspector
    public Image fadeScreen; // UI Image for fade effect
    public float fadeDuration = 0.5f; // Duration of fade effect
    public float disableDuration = 0.6f; // Time before re-enabling player collider

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportWithFade(other));
        }
    }

    private IEnumerator TeleportWithFade(Collider player)
    {
        CharacterController playerController = player.GetComponent<CharacterController>();
        if (playerController != null) playerController.enabled = false; // Disable collision

        yield return StartCoroutine(FadeToBlack());

        player.transform.position = teleportDestination.position;

        yield return new WaitForSeconds(disableDuration); // Prevent instant retrigger

        if (playerController != null) playerController.enabled = true; // Re-enable collision

        yield return StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeScreen.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            fadeScreen.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1;
        fadeScreen.color = color;
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        Color color = fadeScreen.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            fadeScreen.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 0;
        fadeScreen.color = color;
    }
}
