using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Game Is Exiting...");
        Application.Quit();
    }
}
