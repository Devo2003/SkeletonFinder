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

    [Header("Particle Effect")]
    public ParticleSystem burnParticlesPrefab; //Reference
    private ParticleSystem burnParticles; //Instantiate

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

        if (burnParticlesPrefab != null)
        {
            burnParticles = Instantiate(burnParticlesPrefab, transform.position, Quaternion.identity);
            burnParticles.transform.parent = this.transform; //parent to burned object
            burnParticles.Stop(); //Instantiates it but ensures its disabled
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
            if (burnParticles != null)
            {
                Debug.Log("Particles Activated");
                burnParticles.Play();
            }
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

        if (isBurnActive)
        {
            burnLoopSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            burnLoopSFX.release();
            burnPrimeSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Prime");
            burnPrimeSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            burnPrimeSFX.start();

            burnPrimeSFX.release();
            burnLoopSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Loop");
            burnLoopSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            burnLoopSFX.start();
        }
        else
        {
            burnLoopSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            burnLoopSFX.release();
            burnDeactivateSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Burn Deactivate");
            burnDeactivateSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            burnDeactivateSFX.start();
        }
    }

    private void TryBurnObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Burnable"))
            {
                // Position the particle system to the object's center
                burnParticles.transform.position = hit.collider.bounds.center;

                // Force rotation to always face upward (Y-axis)
                burnParticles.transform.rotation = Quaternion.Euler(-90, 0, 0);

                // Optional: Clear and restart the particle system to ensure it plays fresh
                burnParticles.Clear();
                burnParticles.Play();

                // Unparent the particles so they continue playing after destruction
                burnParticles.transform.parent = null;

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
