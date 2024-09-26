using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

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



    //OLD MOVEMENT

    //public float speed = 5f;
    //public float ascend = 0.0f;
    //public float XMovement;
    //public float YMovement;
    //Vector3 Vec;

    //// Update is called once per frame
    //void Update()
    //{
    //    ControllerMovement();
    //}
    //public void ControllerMovement()
    //{
    //    Vec = transform.localPosition;
    //    Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 5;
    //    Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 5;
    //    transform.localPosition = Vec; 
    //}
}
