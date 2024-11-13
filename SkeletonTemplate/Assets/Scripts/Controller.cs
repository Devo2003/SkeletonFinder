using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{

    public Transform player;                 // Reference to the player object
    public Vector3 offset = new Vector3(0, 2, -4); // Default offset from the player
    public float smoothSpeed = 0.125f;        // Smoothness factor for camera movement
    public float rotationSpeed = 100f;        // Speed for camera rotation with the right stick

    private float currentYaw = 0f;            // Current horizontal rotation (yaw) of the camera
    private float currentPitch = 0f;          // Current vertical rotation (pitch) of the camera

    void LateUpdate()
    {
        // Get input for camera rotation from the right joystick
        float horizontalInput = Input.GetAxis("RightStickHorizontal");  // Right stick horizontal axis
        float verticalInput = Input.GetAxis("RightStickVertical");      // Right stick vertical axis

        // Update yaw and pitch based on right stick input
        currentYaw += horizontalInput * rotationSpeed * Time.deltaTime;
        currentPitch -= verticalInput * rotationSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, -30f, 60f); // Limit the pitch angle

        // Calculate offset position with current yaw and pitch
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        Vector3 desiredPosition = player.position + rotation * offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update camera position
        transform.position = smoothedPosition;

        // Keep the camera looking at the player
        transform.LookAt(player.position + Vector3.up * 1.5f); // Slightly above player
    }



    //public float moveSpeed = 5f;           // Movement speed of the player
    //public float rotationSpeed = 720f;     // Rotation speed (degrees per second)
    //public float gravity = -9.81f;         // Gravity force
    //public Transform cameraTransform;      // Reference to the camera's transform

    //private CharacterController controller;
    //private Vector3 velocity;

    //void Start()
    //{
    //    controller = GetComponent<CharacterController>();
    //}

    //void Update()
    //{
    //    // Get input for movement (horizontal and vertical axes)
    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");

    //    // Calculate direction relative to the camera
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    // Only move if there is input
    //    if (direction.magnitude >= 0.1f)
    //    {
    //        // Calculate target angle based on camera direction and player input
    //        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);

    //        // Rotate player to face the target direction
    //        transform.rotation = Quaternion.Euler(0f, angle, 0f);

    //        // Calculate move direction based on the camera's forward direction
    //        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    //        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    //    }

    //    // Apply gravity
    //    if (controller.isGrounded)
    //    {
    //        velocity.y = 0f;
    //    }
    //    else
    //    {
    //        velocity.y += gravity * Time.deltaTime;
    //    }

    //    // Apply vertical velocity
    //    controller.Move(velocity * Time.deltaTime);
    //}
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



