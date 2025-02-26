using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableTelekinesis : TelekinesisObject
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = new Vector3(1, 0, 0); // Default: Move along X-axis
    public float moveDistance = 3f; // How far it moves
    public float moveSpeed = 2f; // Speed of movement

    [Header("Rotation Settings")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Default: Rotate around Y-axis
    public float rotationSpeed = 90f; // Degrees per second
    public float rotationDuration = 2f; // How long the object rotates

    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * moveDistance;
    }

    public override void ActivateTelekinesis()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveAndRotateObject());
        }
    }

    private IEnumerator MoveAndRotateObject()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(rotationAxis * rotationSpeed) * startRotation;

        while (elapsedTime < moveSpeed)
        {
            // Move object
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveSpeed);

            // Rotate object
            if (rotationSpeed > 0)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final positions are exact
        transform.position = targetPosition;
        if (rotationSpeed > 0)
        {
            transform.rotation = targetRotation;
        }

        isMoving = false;
    }
}
