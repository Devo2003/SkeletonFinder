using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;          // Speed at which the player moves between tiles
    public float snapThreshold = 0.01f;  // Distance threshold for snapping to the target

    [Header("Grid Settings")]
    public Vector2 gridSize = new Vector2(1f, 1f); // Size of each grid cell
    public int gridWidth = 10;                     // Number of columns in the grid
    public int gridHeight = 10;                    // Number of rows in the grid
    public Color gridColor = Color.green;          // Color of the grid lines
    public Vector3 gridOrigin = Vector3.zero;      // Origin point of the grid

    private Vector3 targetPosition;               // The target position for the player
    private bool isMoving = false;                // Tracks if the player is moving

    void Start()
    {
        // Initialize target position to the player's starting position
        targetPosition = transform.position;

        // Optionally set the grid origin to the player's starting position
        //if (gridOrigin == Vector3.zero)
        //{
        //    gridOrigin = transform.position;
        //}
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
            return;
        }

        // Detect keyboard input (WASD or Arrow Keys)
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            input.y = 1;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            input.y = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            input.x = -1;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            input.x = 1;

        // If valid input, calculate the new target position
        if (input != Vector2.zero)
        {
            Vector3 newTarget = transform.position + new Vector3(input.x * gridSize.x, 0, input.y * gridSize.y);

            // Only update target position if it's different from the current one
            if (newTarget != targetPosition)
            {
                targetPosition = newTarget;
                isMoving = true;
            }
        }
    }

    private void MoveTowardsTarget()
    {
        // Smoothly move the character towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the player has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < snapThreshold)
        {
            transform.position = targetPosition; // Snap to the exact position
            isMoving = false;                   // Stop movement
        }
    }

    // Draw the grid using Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // Loop through each cell in the grid
        for (int x = -gridWidth / 2; x <= gridWidth / 2; x++)
        {
            for (int y = -gridHeight / 2; y <= gridHeight / 2; y++)
            {
                Vector3 cellCenter = gridOrigin + new Vector3(x * gridSize.x, 0, y * gridSize.y);

                // Draw the outline of each cell
                Gizmos.DrawLine(cellCenter + new Vector3(-gridSize.x / 2, 0, -gridSize.y / 2),
                                cellCenter + new Vector3(gridSize.x / 2, 0, -gridSize.y / 2));
                Gizmos.DrawLine(cellCenter + new Vector3(gridSize.x / 2, 0, -gridSize.y / 2),
                                cellCenter + new Vector3(gridSize.x / 2, 0, gridSize.y / 2));
                Gizmos.DrawLine(cellCenter + new Vector3(gridSize.x / 2, 0, gridSize.y / 2),
                                cellCenter + new Vector3(-gridSize.x / 2, 0, gridSize.y / 2));
                Gizmos.DrawLine(cellCenter + new Vector3(-gridSize.x / 2, 0, gridSize.y / 2),
                                cellCenter + new Vector3(-gridSize.x / 2, 0, -gridSize.y / 2));
            }
        }
    }

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



