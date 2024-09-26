using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squarepatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of points to patrol (4 points for a square)
    public float speed = 5f;         // Movement speed
    private int currentPointIndex = 0; // Keeps track of the current point

    void Start()
    {
        // Set the initial position of the capsule to the first patrol point
        transform.position = patrolPoints[0].position;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move the capsule towards the current patrol point
        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if the capsule has reached the patrol point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Go to the next point, loop back to the first point after reaching the last
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}
