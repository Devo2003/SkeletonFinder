using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform player;           // Reference to the player object
    public float smoothSpeed = 0.2f;   // Smooth camera movement speed
    public Vector2 roomSize = new Vector2(16f, 9f); // Size of the rooms/grid areas
    public Vector2 mapBoundsMin;       // Minimum bounds of the map
    public Vector2 mapBoundsMax;       // Maximum bounds of the map

    private Vector3 targetPosition;    // The target position for the camera

    void LateUpdate()
    {
        if (player == null)
            return;

        // Get player's position and lock it to the center of the room
        Vector3 playerPos = player.position;
        targetPosition = new Vector3(
            Mathf.Clamp(playerPos.x, mapBoundsMin.x + roomSize.x / 2, mapBoundsMax.x - roomSize.x / 2),
            transform.position.y,
            Mathf.Clamp(playerPos.z, mapBoundsMin.y + roomSize.y / 2, mapBoundsMax.y - roomSize.y / 2)
        );

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    void OnDrawGizmos()
    {
        // Visualize the map bounds in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            new Vector3((mapBoundsMin.x + mapBoundsMax.x) / 2, transform.position.y, (mapBoundsMin.y + mapBoundsMax.y) / 2),
            new Vector3(mapBoundsMax.x - mapBoundsMin.x, 0, mapBoundsMax.y - mapBoundsMin.y)
        );
    }
}
