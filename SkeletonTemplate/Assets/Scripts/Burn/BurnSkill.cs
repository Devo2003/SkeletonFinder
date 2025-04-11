using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSkill : MonoBehaviour
{
    private bool hasBurnSkill = false;
    private bool isBurnActive = false;
    private bool canBurn = true;
    private float burnCooldown = 3f;
    private float cooldownTimer = 0f;

    public Material highlightMaterial; // Assign a glow/highlight material in the Inspector
    private Material defaultMaterial;

    void Update()
    {
        // Toggle burn skill on/off
        if (hasBurnSkill && Input.GetKeyDown(KeyCode.B))
        {
            isBurnActive = !isBurnActive;
            HighlightBurnableObjects(isBurnActive);
            Debug.Log("Burn Skill " + (isBurnActive ? "Activated" : "Deactivated"));
        }

        // Cooldown logic
        if (!canBurn)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canBurn = true;
                Debug.Log("Burn Skill Ready!");
            }
        }

        // Use burn skill with cooldown
        if (isBurnActive && canBurn && Input.GetMouseButtonDown(0))
        {
            TryBurnObject();
        }
    }

    public void CollectBurnSkill()
    {
        hasBurnSkill = true;
        Debug.Log("Burn Skill Collected!");
    }

    private void TryBurnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Burnable"))
            {
                Destroy(hit.collider.gameObject);
                Debug.Log("Object Burned!");

                canBurn = false;
                cooldownTimer = burnCooldown;
                Debug.Log("Burn Skill Cooling Down...");
            }
        }
    }

    // Highlight or reset burnable objects
    private void HighlightBurnableObjects(bool highlight)
    {
        GameObject[] burnableObjects = GameObject.FindGameObjectsWithTag("Burnable");

        foreach (GameObject obj in burnableObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (highlight)
                {
                    defaultMaterial = renderer.material; // Store original material
                    renderer.material = highlightMaterial; // Change to highlight material
                }
                else
                {
                    renderer.material = defaultMaterial; // Reset to original material
                }
            }
        }
    }
}
