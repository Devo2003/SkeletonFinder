using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableObject : MonoBehaviour
{
    public string requiredItem; // The item needed to interact with this object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerInventory.HasItem(requiredItem))
        {
            Destroy(gameObject); // Destroy the object if the player has the required item
        }
    }
}
