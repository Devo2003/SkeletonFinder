using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnSpellPickUp : MonoBehaviour
{

    public Image uiBook_Burn;

    private void Awake()
    {
        uiBook_Burn.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure player has the right tag
        {
            BurnSkill burnSkill = other.GetComponent<BurnSkill>();
            if (burnSkill != null)
            {
                burnSkill.CollectBurnSkill(); // Grant the skill
                Destroy(gameObject); // Remove the pickup
                uiBook_Burn.enabled = true;
            }
        }
    }
}
