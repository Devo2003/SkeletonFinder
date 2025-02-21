using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisPower : MonoBehaviour
{
    public static TelekinesisPower Instance { get; private set; } // Singleton pattern

    public bool powerUnlocked = false; // Becomes true after collecting an item
    public bool powerActive = false; // Becomes true when toggled on

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UnlockPower()
    {
        powerUnlocked = true;
        Debug.Log("Telekinesis Power Unlocked!");
    }

    public void TogglePower()
    {
        if (powerUnlocked)
        {
            powerActive = !powerActive;
            Debug.Log("Telekinesis Power " + (powerActive ? "Enabled" : "Disabled"));
        }
    }
}
