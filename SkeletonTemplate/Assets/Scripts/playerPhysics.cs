using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPhysics : MonoBehaviour
{
    public float speed = 5f;                          // Normal movement speed
    public float uphillSlowdownFactor = 0.5f;         // Slow down factor for uphill movement
    public float downhillStickFactor = 0.1f;          // Factor to stick to the ground when going downhill
    public float groundCheckDistance = 1.1f;          // Distance to check ground
    private CharacterController characterController;   // Character controller reference
    private Vector3 moveDirection;                     // Movement direction

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create movement vector and normalize
        moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // Handle slope effects
        HandleSlope();
        
        // Move the player
        //characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    void HandleSlope()
    {
        // Check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundCheckDistance))
        {
            // Calculate the angle of the slope
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal); // Project movement direction onto the slope
            
            if (angle > 0)
            {
                if (angle < 30) // Uphill threshold (adjust as needed)
                {
                    // Slow down on uphill
                    moveDirection = slopeDirection * speed * uphillSlowdownFactor; // Apply uphill slowdown
                }
                else // Downhill
                {
                    // Stick to the ground when going downhill
                    moveDirection = slopeDirection * speed; // Keep speed for downhill but adjust direction
                    moveDirection.y = -downhillStickFactor; // Apply a slight downward force to stick to the ground
                }
            }
        }
        else
        {
            // Apply gravity if not grounded
            moveDirection.y -= 9.81f * Time.deltaTime; // Adjust gravity as needed
        }
    }

}
