using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject gameOverText;
    public Button retryButton;
    public Button quitButton;

    private void Start()
    {
        HideMenu(); // Hide the menu initially
        retryButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowGameOver()
    {
        Debug.Log("Game Over Menu Shown");

        // Show the game over UI
        gameOverText.SetActive(true);
        retryButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        // Freeze the game
        Time.timeScale = 0;
    }

    public void HideMenu()
    {
        gameOverText.SetActive(false);
        retryButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        // Reset the game speed before restarting
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


