using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSpellPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure player has the right tag
        {
            BurnSkill burnSkill = other.GetComponent<BurnSkill>();
            if (burnSkill != null)
            {
                burnSkill.CollectBurnSkill(); // Grant the skill
                Destroy(gameObject); // Remove the pickup
            }
        }
    }
}
