using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    public static TelekinesisController Instance;
    public bool isTelekinesisActive = false; // Toggle state
    public KeyCode toggleKey = KeyCode.T; // Key to toggle telekinesis
    private TelekinesisObject selectedObject;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isTelekinesisActive = !isTelekinesisActive;
            Debug.Log("Telekinesis " + (isTelekinesisActive ? "Activated" : "Deactivated"));
        }

        if (isTelekinesisActive && Input.GetMouseButtonDown(0)) // Left-click to select
        {
            SelectObject();
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
}
