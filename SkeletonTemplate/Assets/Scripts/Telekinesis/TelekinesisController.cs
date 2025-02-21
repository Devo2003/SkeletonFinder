using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisController : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.T; // Key to toggle telekinesis
    public float moveDistance = 3f; // How far the object moves
    public float moveSpeed = 2f; // Speed of movement
    public float moveDuration = 2f; // How long the movement lasts

    private TelekinesisObject selectedObject; // Currently selected object

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            TelekinesisPower.Instance.TogglePower();
        }

        if (TelekinesisPower.Instance.powerActive && Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }
    }

    private void SelectObject()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No Main Camera found! Make sure your camera has the 'MainCamera' tag.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            TelekinesisObject obj = hit.collider.GetComponent<TelekinesisObject>();

            if (obj != null)
            {
                selectedObject = obj;
                Debug.Log("Selected: " + obj.gameObject.name);
                StartMovingObject();
            }
            else
            {
                Debug.Log("No valid TelekinesisObject found on " + hit.collider.gameObject.name);
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }

    private void StartMovingObject()
    {
        if (selectedObject == null)
        {
            Debug.LogError("No object selected for telekinesis!");
            return;
        }

        Vector3 targetPosition = selectedObject.transform.position + (Vector3.forward * moveDistance);
        selectedObject.StartMovement(targetPosition, moveSpeed, moveDuration);
    }
}
