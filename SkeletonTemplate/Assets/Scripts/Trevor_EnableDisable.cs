using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trevor_EnableDisable : MonoBehaviour
{
    public float timerDuration = 3f; // Duration before switching objects
    public GameObject objectToEnable; // Object to enable after timer ends

    private bool timerActive = false;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!timerActive) // Prevent starting the timer multiple times
        {
            timerActive = true;
            timer = timerDuration;
            Debug.Log("Timer started");
        }
    }

    private void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timerActive = false;
                DisableInitialObject();
            }
        }
    }

    private void DisableInitialObject()
    {
        Debug.Log("Timer ended. Disabling object and enabling new one.");
        gameObject.SetActive(false); // Disable this object

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true); // Enable the new object
        }
    }
}
