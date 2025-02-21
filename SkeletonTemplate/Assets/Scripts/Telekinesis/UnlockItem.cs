using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TelekinesisPower.Instance.UnlockPower();
            Destroy(gameObject); // Remove the item after collection
        }
    }
}
