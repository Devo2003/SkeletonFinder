using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveableTelekinesis : TelekinesisObject
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    [Header("Telekinesis Timer")]
    public float telekinesisDuration = 5f;
    private float timer = 0f;
    private bool isBeingMoved = false;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject telekinesisIndicator;

    private Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer)
        {
            originalColor = objectRenderer.material.color;
        }
    }

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
        if (!TelekinesisController.Instance.isTelekinesisActive) return;

        isBeingMoved = true;
        timer = telekinesisDuration;

        if (telekinesisIndicator) telekinesisIndicator.SetActive(true);
        RemoveHighlight();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveX = 1f;

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        float rotateY = 0f;

        if (Input.GetKey(KeyCode.Q)) rotateY = -1f;
        if (Input.GetKey(KeyCode.E)) rotateY = 1f;

        transform.Rotate(Vector3.up * rotateY * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timerText)
        {
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}s";
        }

        if (timer <= 0)
        {
            isBeingMoved = false;
            if (telekinesisIndicator) telekinesisIndicator.SetActive(false);
            if (timerText) timerText.text = "";
        }
    }


    private void OnMouseEnter()
    {
        if (TelekinesisController.Instance.isTelekinesisActive && !isBeingMoved && objectRenderer)
        {
            objectRenderer.material.color = Color.cyan;
        }
    }

    private void OnMouseExit()
    {
        if (objectRenderer)
        {
            objectRenderer.material.color = originalColor;
        }
    }

    private void RemoveHighlight()
    {
        if (objectRenderer)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}




