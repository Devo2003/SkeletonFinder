using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveableTelekinesis : TelekinesisObject
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;      // Speed of movement
    public float rotationSpeed = 100f; // Speed of rotation

    [Header("Telekinesis Timer")]
    public float telekinesisDuration = 5f; // Time before deselecting (editable in Inspector)
    private float timer = 0f;
    private bool isBeingMoved = false;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;  // UI Text for countdown
    public GameObject telekinesisIndicator; // UI Indicator (e.g., a panel or icon)

    private void Update()
    {
        if (isBeingMoved)
        {
            HandleMovement();
            HandleRotation();
            UpdateTimer();
        }
    }

    public override void ActivateTelekinesis()
    {
        isBeingMoved = true;
        timer = telekinesisDuration; // Reset timer when activated

        if (telekinesisIndicator) telekinesisIndicator.SetActive(true); // Show UI indicator
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) moveZ = -1f; // Move left
        if (Input.GetKey(KeyCode.RightArrow)) moveZ = 1f; // Move right
        if (Input.GetKey(KeyCode.UpArrow)) moveX = -1f; // Move up
        if (Input.GetKey(KeyCode.DownArrow)) moveX = 1f; // Move down

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        float rotateY = 0f;

        if (Input.GetKey(KeyCode.Q)) rotateY = -1f; // Rotate left
        if (Input.GetKey(KeyCode.E)) rotateY = 1f;  // Rotate right

        transform.Rotate(Vector3.up * rotateY * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        // Update UI Timer
        if (timerText)
        {
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}s";
        }

        // Deselect object when time runs out
        if (timer <= 0)
        {
            isBeingMoved = false;
            if (telekinesisIndicator) telekinesisIndicator.SetActive(false); // Hide UI indicator
            if (timerText) timerText.text = ""; // Clear timer UI
        }
    }
}




