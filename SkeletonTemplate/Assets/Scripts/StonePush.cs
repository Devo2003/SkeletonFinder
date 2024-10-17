using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StonePush : MonoBehaviour
{

    public Transform gravestone;          // Reference to the gravestone
    public Vector3 moveDirection = new Vector3(0, 0, -1); // Direction to move the gravestone
    public float moveDistance = 2f;       // Distance the gravestone should move
    public float moveSpeed = 2f;          // Speed of the gravestone movement
    public GameObject keyObject;          // Reference to the key object (hidden initially)

    private Vector3 targetPosition;       // Target position for the gravestone
    private bool isMoving = false;        // Flag to control when to move the gravestone
    private bool keyRevealed = false;     // Flag to ensure the key is revealed only once

    void Start()
    {
        // Set the target position based on the move direction and distance
        targetPosition = gravestone.position + moveDirection.normalized * moveDistance;

        // Hide the key at the start
        keyObject.SetActive(false);
    }

    void Update()
    {
        // Move the gravestone if it's supposed to move
        if (isMoving)
        {
            // Move the gravestone towards the target position
            gravestone.position = Vector3.MoveTowards(gravestone.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the gravestone has reached the target position
            if (Vector3.Distance(gravestone.position, targetPosition) <= 0.01f)
            {
                isMoving = false;

                // Reveal the key if it hasn't been revealed yet
                if (!keyRevealed)
                {
                    keyObject.SetActive(true);  // Make the key visible
                    keyRevealed = true;        // Ensure the key is only revealed once
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Start moving the gravestone if the player touches it
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
        }

    }
}

