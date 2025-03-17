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

    [Header("Particle Effect")]
    public ParticleSystem movementParticlesPrefab; //Reference
    private ParticleSystem movementParticles; //Instantiate

    private Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer)
        {
            originalColor = objectRenderer.material.color;
        }

        if (movementParticlesPrefab != null)
        {
            movementParticles = Instantiate(movementParticlesPrefab, transform.position, Quaternion.identity);
            movementParticles.Stop(); //Instantiates it but ensures its disabled
        }
    }

    private void Update()
    {
        if (isBeingMoved)
        {
            HandleMovement();
            UpdateTimer();
        }
    }

    public override void ActivateTelekinesis()
    {
        if (!TelekinesisController.Instance.isTelekinesisActive) return;
        if (movementParticles != null)
        {
            Debug.Log("Particles Activated");
            movementParticles.Play();
        }
        isBeingMoved = true;
        timer = telekinesisDuration;

        if (telekinesisIndicator) telekinesisIndicator.SetActive(true);
        RemoveHighlight();
    }


    private bool particlesActive = false;
    private void HandleMovement()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get target position based on raycast hit but lock Y position
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

            // Move toward target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (movementParticles != null)
            {
                movementParticles.transform.position = transform.position;

                if (!particlesActive)
                {
                    Debug.Log("Particles Activated");
                    movementParticles.Play();
                    particlesActive = true;
                }
            }
        }
    }

    private bool wasBeingMoved = false;
    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timerText)
        {
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}s";
        }

        if (timer <= 0)
        {
            if (isBeingMoved) //stops particles when TK timer ends
            {

                isBeingMoved = false;
                if (telekinesisIndicator) telekinesisIndicator.SetActive(false);
                if (timerText) timerText.text = "";

                TelekinesisController.Instance.EndTelekinesis(); //Begin Cooldown AFTER use

                if (movementParticles != null && particlesActive)
                {
                    Debug.Log("Particles Stopped");
                    movementParticles.Stop();
                    particlesActive = false;
                }
            }
        }

        wasBeingMoved = isBeingMoved;

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

    public void ToggleHighlight(bool state)
    {
        if (objectRenderer)
        {
            objectRenderer.material.color = state ? Color.cyan : originalColor;
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




