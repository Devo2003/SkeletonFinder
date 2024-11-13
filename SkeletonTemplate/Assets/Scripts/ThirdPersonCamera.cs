using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;               // Reference to the player object
    public Vector3 offset = new Vector3(0, 2, -4); // Default offset from the player
    public float smoothSpeed = 0.125f;      // Smoothness factor for camera movement
    public float rotationSpeed = 5.0f;      // Speed for camera rotation

    void LateUpdate()
    {
        // Get player input for camera rotation (e.g., right joystick or mouse)
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotate the offset around the player based on input
        offset = Quaternion.AngleAxis(horizontalInput, Vector3.up) * offset;

        // Calculate the desired position based on player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update camera position
        transform.position = smoothedPosition;

        // Make sure the camera is always looking at the player
        transform.LookAt(player.position + Vector3.up * 1.5f); // Look at a point slightly above player
    }
}



