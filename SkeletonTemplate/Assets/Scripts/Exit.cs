using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string requiredItemName = "Key";  // The name of the required item
    public string nextLevelName = "Level2";  // The name of the next scene/level to load

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TorsoFunction playerInventory = other.gameObject.GetComponent<TorsoFunction>();

            if (playerInventory != null && HasRequiredItem(playerInventory))
            {
                UnlockGate();
            }
            else
            {
                Debug.Log("You need the " + requiredItemName + " to unlock the gate.");
            }
        }
    }

    bool HasRequiredItem(TorsoFunction inventory)
    {
        return inventory.inventoryItems.Contains(requiredItemName);
    }

    void UnlockGate()
    {
        Debug.Log("Gate unlocked! Loading next level");
        SceneManager.LoadScene(nextLevelName);  // Load the next scene
    }
}
