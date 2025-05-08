using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectibleType { Key, Egg, Bone, Spellbook }
public class CollectibleItem : MonoBehaviour
{

    public CollectibleType type;
    public string itemName; // Name of the item to be collected
    public GameObject inspectModelPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Collected: {itemName}, Showing model: {inspectModelPrefab?.name}");
            PlayerInventory.CollectItem(itemName);
            InspectUI.Instance.InspectItem(itemName, inspectModelPrefab);
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