using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelekinesisController : MonoBehaviour
{
    public static TelekinesisController Instance;
    public bool isTelekinesisActive = false; // Toggle state
    public Button telekinesisButton; // UI Button to prime telekinesis
    public Image telekinesisBox;
    public Outline buttonOutline; // Outline component for glow effect

    private TelekinesisObject selectedObject;
    private bool hasSelectedObject = false;
    private bool hasTelekinesis = false; // Track if telekinesis is unlocked

    [Header("Cooldown Settings")]
    public float telekinesisCooldown = 3f;
    private bool isCooldownActive = false;
    public Image cooldownImage; // Ensure this is an Image with "Fill" set in its Image component

    public FMOD.Studio.EventInstance telekinesisPrimeSFX;
    public FMOD.Studio.EventInstance telekinesisLoopSFX;
    public FMOD.Studio.EventInstance telekinesisDeactivateSFX;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        cooldownImage.enabled = false; // Initially disabled
        telekinesisBox.enabled = false;

        if (telekinesisButton != null)
        {
            telekinesisButton.onClick.AddListener(ToggleTelekinesis); // Assign button listener
            buttonOutline = telekinesisButton.GetComponent<Outline>(); // Get Outline component
            telekinesisButton.interactable = false; // Disable until unlocked
        }
    }

    private void Update()
    {
        if (isTelekinesisActive && Input.GetMouseButtonDown(0)) // Left-click to select
        {
            SelectObject();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTelekinesis();
        }
    }

    private void ToggleTelekinesis()
    {
        if (!hasTelekinesis || isCooldownActive) return; // Prevent activation if not unlocked or on cooldown

        isTelekinesisActive = !isTelekinesisActive;
        Debug.Log("Telekinesis " + (isTelekinesisActive ? "Activated" : "Deactivated"));

        telekinesisPrimeSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Telekinesis Prime");
        telekinesisPrimeSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        telekinesisPrimeSFX.start();


        //Highlight Selectable Objects
        foreach (MoveableTelekinesis obj in FindObjectsOfType<MoveableTelekinesis>())
        {
            obj.ToggleHighlight(isTelekinesisActive);
        }

        if (buttonOutline != null)
        {
            buttonOutline.effectColor = isTelekinesisActive ? Color.cyan : Color.clear;
            buttonOutline.effectDistance = isTelekinesisActive ? new Vector2(5, 5) : new Vector2(0, 0);
        } 
        
        //Clear all highlights when telekinesis is inactive
        if (!isTelekinesisActive)
        {
            ClearAllHighlights();
        }

        // Remove focus to prevent spacebar triggering
        telekinesisButton.Select();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    private void ClearAllHighlights()
    {
        foreach (MoveableTelekinesis obj in FindObjectsOfType<MoveableTelekinesis>())
        {
            obj.ToggleHighlight(false);
        }
    }

    private IEnumerator TelekinesisCooldown()
    {

        isCooldownActive = true;
        cooldownImage.enabled = true;

        float cooldown = telekinesisCooldown;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            cooldownImage.fillAmount = cooldown / telekinesisCooldown; // Update the fill amount of the image

            yield return null;
        }

        isCooldownActive = false;
        isTelekinesisActive = false;
        cooldownImage.enabled = false; // Disable the image once cooldown is over

        if (buttonOutline != null)
        {
            buttonOutline.effectColor = Color.clear;
            buttonOutline.effectDistance = new Vector2(0, 0);
        }

        hasSelectedObject = false;
        ClearAllHighlights();
    }

    private void SelectObject()
    {

        if (hasSelectedObject || Camera.main == null) return; //Blocks multiple selections
        
        if (Camera.main == null)
        {
            Debug.LogError("No Main Camera found! Ensure your camera is tagged as 'MainCamera'!");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TelekinesisObject obj = hit.collider.GetComponent<TelekinesisObject>();

            if (obj != null)
            {
                telekinesisPrimeSFX.release();
                telekinesisLoopSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Telekinesis Loop");
                telekinesisLoopSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                telekinesisLoopSFX.start();

                Debug.Log("Selected Telekinesis Object: " + obj.gameObject.name);
                selectedObject = obj;
                selectedObject.ActivateTelekinesis(); // Let the object handle movement

                hasSelectedObject = true;
                ClearAllHighlights();
            }
            else
            {
                Debug.Log("No valid TelekinesisObject found on " + hit.collider.gameObject.name);
            }
        }
    }

    public void EndTelekinesis()
    {

        if (isTelekinesisActive)
        {
            telekinesisLoopSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            telekinesisLoopSFX.release();
            telekinesisDeactivateSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Telekinesis Deactivate");
            telekinesisDeactivateSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            telekinesisDeactivateSFX.start();

            StartCoroutine(TelekinesisCooldown()); //Start cooldown after use ends
        }

    }

    // Call this method when the player collects the spellbook
    public void UnlockTelekinesis()
    {
        hasTelekinesis = true;
        if (telekinesisButton != null)
        {
            telekinesisButton.interactable = true;
            telekinesisBox.enabled = true;
        }
    }
}

