using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squarepatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array of points to patrol (4 points for a square)
    public float speed = 5f;         // Movement speed
    private int currentPointIndex = 0; // Keeps track of the current point
    private float rotationSpeed = 10f;
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
        // Get the current patrol point
        Transform targetPoint = patrolPoints[currentPointIndex];

        // Calculate direction to the patrol point
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // If not at the patrol point, move towards it
        if (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
        {
            // Move the capsule towards the current patrol point
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            // Smoothly rotate the enemy to face the patrol point
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100f);
        }
        else
        {
            // Once the enemy reaches the patrol point, switch to the next point
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}
