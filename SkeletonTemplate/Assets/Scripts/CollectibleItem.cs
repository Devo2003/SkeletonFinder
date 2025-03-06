using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem: MonoBehaviour
{
    public string itemName; // Name of the item to be collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.CollectItem(itemName);
            Destroy(gameObject); // Destroy item after collection
        }
    }
}

public static class PlayerInventory
{
    private static HashSet<string> collectedItems = new HashSet<string>();

    public static void CollectItem(string itemName)
    {
        collectedItems.Add(itemName);
    }

    public static bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }
}

