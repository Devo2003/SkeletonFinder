using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; // Singleton reference

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 15f;
    public float rotationSpeed = 10f;

    public FMOD.Studio.EventInstance movementSFX;
    public string witchSpeed = "WitchSpeed";

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Transform playerModel;
    private bool canMove = true; // Movement state

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Missing Rigidbody on " + gameObject.name);
            return;
        }

        rb.freezeRotation = true;
        playerModel = transform.GetChild(0);

        movementSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Witch Movement");
        movementSFX.start();
    }

    void Update()
    {
        if (canMove)
        {
            ProcessInput();
            movementSFX.setParameterByName(witchSpeed, (Mathf.Abs(moveVelocity.x) + Mathf.Abs(moveVelocity.z)));
            movementSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        }
    }

    void FixedUpdate()
    {
        if (canMove)
            MovePlayer();
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
        if (!enable)
        {
            rb.velocity = Vector3.zero;
            moveVelocity = Vector3.zero;
        }
    }

    void ProcessInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            input = new Vector2(-1, 0);
        else if (Input.GetKey(KeyCode.S))
            input = new Vector2(1, 0);
        else if (Input.GetKey(KeyCode.A))
            input = new Vector2(0, -1);
        else if (Input.GetKey(KeyCode.D))
            input = new Vector2(0, 1);

        moveInput = new Vector3(input.x, 0, input.y).normalized;
    }

    void MovePlayer()
    {
        if (moveInput.magnitude > 0)
        {
            moveVelocity = Vector3.MoveTowards(moveVelocity, moveInput * moveSpeed, acceleration * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            moveVelocity = Vector3.MoveTowards(moveVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }
}