using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnSkill : MonoBehaviour
{
    private bool hasBurnSkill = false;
    private bool isBurnActive = false;
    private bool canBurn = true;
    private float burnCooldown = 3f;
    private float cooldownTimer = 0f;

    public Image burnBox;

    public Material highlightMaterial; // Assign a glow/highlight material in the Inspector
    private Material defaultMaterial;

    [Header("UI")]
    public Button burnButton;
    public Image cooldownOverlay;

    public FMOD.Studio.EventInstance burnPrimeSFX;
    public FMOD.Studio.EventInstance burnLoopSFX;
    public FMOD.Studio.EventInstance burnDeactivateSFX;



    private void Awake()
    {
        burnBox.enabled = false;
    }
    private void Start()
    {
        if (burnButton != null)
        {
            burnButton.onClick.AddListener(ToggleBurnSkill);
        }

        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (!hasBurnSkill) return;

        // Keyboard toggle
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBurnSkill();
        }

        // Handle cooldown
        if (!canBurn)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = cooldownTimer / burnCooldown;

            if (cooldownTimer <= 0f)
            {
                canBurn = true;
                if (cooldownOverlay != null)
                    cooldownOverlay.fillAmount = 0f;

                if (burnButton != null)
                    burnButton.interactable = true;

                Debug.Log("Burn Skill Ready!");
            }
        }

        // Use burn skill
        if (isBurnActive && canBurn && Input.GetMouseButtonDown(0))
        {
            TryBurnObject();
        }
    }

    public void CollectBurnSkill()
    {
        hasBurnSkill = true;
        burnBox.enabled = true;
        Debug.Log("Burn Skill Collected!");
        if (burnButton != null)
            burnButton.interactable = true;
    }

    private void ToggleBurnSkill()
    {
        if (!canBurn) return;

        isBurnActive = !isBurnActive;
        HighlightBurnableObjects(isBurnActive);
        Debug.Log("Burn Skill " + (isBurnActive ? "Activated" : "Deactivated"));

        burnPrimeSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Prime");
        burnPrimeSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        burnPrimeSFX.start();

        burnPrimeSFX.release();
        burnLoopSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Loop");
        burnLoopSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        burnLoopSFX.start();
    }

    private void TryBurnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Burnable"))
            {
                burnLoopSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                burnLoopSFX.release();
                burnDeactivateSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Deactivate");
                burnDeactivateSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                burnDeactivateSFX.start();

                Destroy(hit.collider.gameObject);
                Debug.Log("Object Burned!");

                isBurnActive = false;
                HighlightBurnableObjects(false);

                canBurn = false;
                cooldownTimer = burnCooldown;

                Debug.Log("Burn Skill Cooling Down...");

                // Optionally grey out button during cooldown
                if (burnButton != null)
                    burnButton.interactable = false;
            }
        }
    }

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
                    defaultMaterial = renderer.material;
                    renderer.material = highlightMaterial;
                }
                else
                {
                    renderer.material = defaultMaterial;
                }
            }
        }
    }
}
