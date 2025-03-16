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
    public Outline buttonOutline; // Outline component for glow effect

    private TelekinesisObject selectedObject;
    private bool hasTelekinesis = false; // Track if telekinesis is unlocked

    [Header("Cooldown Settings")]
    public float telekinesisCooldown = 3f;
    private bool isCooldownActive = false;
    public TextMeshProUGUI cooldownText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

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
    }

    private void ToggleTelekinesis()
    {
        if (!hasTelekinesis || isCooldownActive) return; // Prevent activation if not unlocked or on cooldown

        isTelekinesisActive = !isTelekinesisActive;
        Debug.Log("Telekinesis " + (isTelekinesisActive ? "Activated" : "Deactivated"));

        if (buttonOutline != null)
        {
            buttonOutline.effectColor = isTelekinesisActive ? Color.cyan : Color.clear;
            buttonOutline.effectDistance = isTelekinesisActive ? new Vector2(5, 5) : new Vector2(0, 0);
        }

        if (isTelekinesisActive)
        {
            StartCoroutine(TelekinesisCooldown());
        }

        // Remove focus to prevent spacebar triggering
        telekinesisButton.Select();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    private IEnumerator TelekinesisCooldown()
    {
        isCooldownActive = true;

        float cooldown = telekinesisCooldown;
        while (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldownText != null)
            {
                cooldownText.text = $"Cooldown: {Mathf.Ceil(cooldown)}s";
            }
            yield return null;
        }

        isCooldownActive = false;
        isTelekinesisActive = false;

        if (buttonOutline != null)
        {
            buttonOutline.effectColor = Color.clear;
            buttonOutline.effectDistance = new Vector2(0, 0);
        }

        if (cooldownText != null)
        {
            cooldownText.text = "";
        }
    }

    private void SelectObject()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No Main Camera found! Ensure your camera is tagged as 'MainCamera'.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TelekinesisObject obj = hit.collider.GetComponent<TelekinesisObject>();

            if (obj != null)
            {
                Debug.Log("Selected Telekinesis Object: " + obj.gameObject.name);
                selectedObject = obj;
                selectedObject.ActivateTelekinesis(); // Let the object handle movement
            }
            else
            {
                Debug.Log("No valid TelekinesisObject found on " + hit.collider.gameObject.name);
            }
        }
    }

    // Call this method when the player collects the spellbook
    public void UnlockTelekinesis()
    {
        hasTelekinesis = true;
        if (telekinesisButton != null)
        {
            telekinesisButton.interactable = true;
        }
    }
}

