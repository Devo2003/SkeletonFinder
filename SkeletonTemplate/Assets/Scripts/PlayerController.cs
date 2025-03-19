using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Max speed
    public float acceleration = 10f;   // How fast we reach max speed
    public float deceleration = 15f;   // How fast we slow down
    public float rotationSpeed = 10f;  // How fast the character turns

    // Used for implementing witch movement SFX
    public FMOD.Studio.EventInstance movementSFX;
    public string witchSpeed = "WitchSpeed";

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Transform playerModel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody has the right settings
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody on " + gameObject.name);
            return;
        }

        rb.freezeRotation = true; // Prevent unwanted rotation

        // Assuming your player model is a child of the main object
        playerModel = transform.GetChild(0); // Assuming the model is the first child

        movementSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Witch Movement");
        movementSFX.start();
    }

    void Update()
    {
        ProcessInput();
        movementSFX.setParameterByName(witchSpeed, (Mathf.Abs(moveVelocity.x) + Mathf.Abs(moveVelocity.z)));
        movementSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void ProcessInput()
    {
        Vector2 input = Vector2.zero;

        // Prioritize vertical movement
        if (Input.GetKey(KeyCode.W))
            input = new Vector2(-1, 0);
        else if (Input.GetKey(KeyCode.S))
            input = new Vector2(1, 0);
        else if (Input.GetKey(KeyCode.A))
            input = new Vector2(0, -1);
        else if (Input.GetKey(KeyCode.D))
            input = new Vector2(0, 1);

        // Convert to world space movement
        moveInput = new Vector3(input.x, 0, input.y).normalized;
    }

    void MovePlayer()
    {
        if (moveInput.magnitude > 0)
        {
            // Accelerate towards max speed
            moveVelocity = Vector3.MoveTowards(moveVelocity, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);

            // Rotate the player model to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            // Decelerate when no input is given
            moveVelocity = Vector3.MoveTowards(moveVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply movement with momentum
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }
}




