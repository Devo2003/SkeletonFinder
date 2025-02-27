using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public static bool hasKey = false; // Tracks if the player has the key

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasKey = true; // Player now has the key
            Destroy(gameObject); // Remove key from scene
            Debug.Log("Key Collected!");
        }
    }
}
