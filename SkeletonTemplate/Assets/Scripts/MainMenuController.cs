using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start the game by loading the main game scene
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Load the credits scene
    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    // Load the controls scene
    public void OpenControls()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Exit the game (only works in builds)
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
