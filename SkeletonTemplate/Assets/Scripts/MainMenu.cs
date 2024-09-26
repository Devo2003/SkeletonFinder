using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    void Update()
    {
        CheckMouseClick();
    }

    public void ButtonFunctions()
    {
        if (gameObject.name == "Start")
        {
            SceneManager.LoadScene("Game");
        }

        if (gameObject.name == "Quit")
        {
            Application.Quit();
        }
    }

    public void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            // Cast a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo; // Variable to store information about the hit

            // Perform the raycast
            if (Physics.Raycast(ray, out hitInfo))
            {
                // Check if the ray hits a button object
                if (hitInfo.collider.CompareTag("Button"))
                {
                    // Perform button action (e.g., load scene, execute function)
                    hitInfo.collider.GetComponent<MainMenu>().ButtonFunctions();
                }
            }
        }
    }

}
