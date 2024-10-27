using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //OLD MOVEMENT

    public float moveSpeed = 5f;    // Base movement speed
    public float lerpSpeed = 10f;   // Speed at which we interpolate using Lerp
    public Rigidbody rb;            // Reference to Rigidbody component

    private Vector3 forward, right; // Directions for isometric movement

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Set the movement directions for an isometric grid
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);
    }

    void Update()
    {
        Vector3 direction = GetInput();

        if (direction.magnitude > 0.1f)
        {
            Move(direction);
        }
        else
        {
            // Stop movement if no input is detected
            rb.velocity = Vector3.zero;
        }
    }

    // Get input from the player (WASD or Arrow keys)
    Vector3 GetInput()
    {
        Vector3 rightMovement = right * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * Input.GetAxis("Vertical");

        return Vector3.Normalize(rightMovement + upMovement);
    }

    // Smoothly move the character by setting velocity
    void Move(Vector3 direction)
    {
        // Calculate the velocity based on the direction and movement speed
        Vector3 targetVelocity = direction * moveSpeed;

        // Apply the velocity to the Rigidbody
        rb.velocity = targetVelocity;
    }





    //public float moveSpeed = 5f;    // Base movement speed
    //public float lerpSpeed = 10f;   // Speed at which we interpolate using Lerp
    //public Rigidbody rb;            // Reference to Rigidbody component

    //private Vector3 forward, right; // Directions for isometric movement

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    // Set the movement directions for an isometric grid
    //    forward = Camera.main.transform.forward;
    //    forward.y = 0;
    //    forward = Vector3.Normalize(forward);

    //    right = Camera.main.transform.right;
    //    right.y = 0;
    //    right = Vector3.Normalize(right);
    //}

    //void Update()
    //{
    //    Vector3 direction = GetInput();

    //    if (direction.magnitude > 0.1f)
    //    {
    //        Move(direction);
    //    }
    //    else
    //    {
    //        // Stop movement if no input is detected
    //        rb.velocity = Vector3.zero;
    //    }
    //}

    //// Get input from the player (WASD, Arrow keys, or Xbox controller)
    //Vector3 GetInput()
    //{
    //    // Get keyboard input
    //    Vector3 rightMovement = right * Input.GetAxis("Horizontal"); // A/D or left/right arrow keys
    //    Vector3 upMovement = forward * Input.GetAxis("Vertical"); // W/S or up/down arrow keys

    //    // Get Xbox controller input
    //    float joystickHorizontal = Input.GetAxis("JoystickHorizontal"); // Set up this axis in Input Manager
    //    float joystickVertical = Input.GetAxis("JoystickVertical"); // Set up this axis in Input Manager

    //    // Combine both input types
    //    Vector3 joystickMovement = right * joystickHorizontal + forward * joystickVertical;

    //    return Vector3.Normalize(rightMovement + upMovement + joystickMovement);
    //}

    //// Smoothly move the character by setting velocity
    //void Move(Vector3 direction)
    //{
    //    // Calculate the velocity based on the direction and movement speed
    //    Vector3 targetVelocity = direction * moveSpeed;

    //    // Apply the velocity to the Rigidbody
    //    rb.velocity = targetVelocity;
    //}


}
