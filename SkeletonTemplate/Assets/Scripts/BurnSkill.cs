using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSkill : MonoBehaviour
{
    private bool hasBurnSkill = false;
    private bool isBurnActive = false;

    void Update()
    {
        // Toggle burn skill on/off
        if (hasBurnSkill && Input.GetKeyDown(KeyCode.B))
        {
            isBurnActive = !isBurnActive;
            Debug.Log("Burn Skill " + (isBurnActive ? "Activated" : "Deactivated"));
        }

        // Use burn skill on click
        if (isBurnActive && Input.GetMouseButtonDown(0))
        {
            TryBurnObject();
        }
    }

    // Collect the burn skill
    public void CollectBurnSkill()
    {
        hasBurnSkill = true;
        Debug.Log("Burn Skill Collected!");
    }

    // Check for burnable objects
    private void TryBurnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Burnable"))
            {
                Destroy(hit.collider.gameObject);
                Debug.Log("Object Burned!");
            }
        }
    }
}
