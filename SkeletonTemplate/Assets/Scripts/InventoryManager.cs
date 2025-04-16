using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int eggCount = 0;
    public int eggsNeededForKey = 3;

    public GameObject keyObjectToEnable; // Assign this in the Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddEgg()
    {
        eggCount++;
        Debug.Log("Egg Count: " + eggCount);

        if (eggCount == eggsNeededForKey)
        {
            EnableKey();
        }
    }

    private void EnableKey()
    {
        if (keyObjectToEnable != null)
        {
            keyObjectToEnable.SetActive(true);
            Debug.Log("Key Enabled!");
        }
        else
        {
            Debug.LogError("No key object assigned to enable!");
        }
    }
}
