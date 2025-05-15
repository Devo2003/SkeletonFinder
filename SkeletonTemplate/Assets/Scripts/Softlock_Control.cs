using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Softlock_Control : MonoBehaviour
{
    public Button mainMenuButton;        // Reference to the button in the UI
    public string mainMenuSceneName = "MainMenu"; // Name of your Main Menu scene

    private void Start()
    {
        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(false); // Hide the button initially
            mainMenuButton.onClick.AddListener(ReturnToMainMenu); // Add the listener
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player colliding
        {
            mainMenuButton.gameObject.SetActive(true); // Show the button
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName); // Load the Main Menu scene
    }
}
