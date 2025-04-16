using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public static bool hasKey = false;
    public static bool hasKey2 = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            string itemType = other.gameObject.name.ToLower();

            if (itemType.Contains("key2"))
            {
                hasKey2 = true;
                Debug.Log("Key 2 Collected!");
            }
            else if (itemType.Contains("key"))
            {
                hasKey = true;
                Debug.Log("Key Collected!");
            }
            else if (itemType.Contains("egg"))
            {
                InventoryManager.Instance.AddEgg();
                Debug.Log("Egg Collected!");
            }

            Destroy(other.gameObject);
        }
    }
}
