using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    //public float pushRadius = 5f;  // Radius of the force push
    //public float pushForce = 1000f; // Strength of the push
    //public LayerMask affectedLayers; // Layers affected by the push
    //public ParticleSystem pushEffect; // Visual effect for the push

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F)) // Trigger the force push
    //    {
    //        PerformForcePush();
    //    }
    //}

    //void PerformForcePush()
    //{
    //    // Show visual effect
    //    if (pushEffect != null)
    //    {
    //        pushEffect.Play();
    //    }

    //    // Find objects in range
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius, affectedLayers);
    //    foreach (Collider collider in colliders)
    //    {
    //        Rigidbody rb = collider.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            // Calculate direction and apply force
    //            Vector3 direction = (collider.transform.position - transform.position).normalized;
    //            rb.AddForce(direction * pushForce, ForceMode.Impulse);
    //        }
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    // Draw the push radius for debugging
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, pushRadius);
    //}

    public Vector3 boxSize = new Vector3(3f, 2f, 5f); // Box dimensions (Width, Height, Depth)
    public float pushForce = 1000f; // Strength of the push
    public LayerMask affectedLayers; // Layers affected by the push
    public ParticleSystem pushEffect; // Visual effect for the push

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Trigger the force push
        {
            PerformForcePush();
        }
    }

    void PerformForcePush()
    {
        // Show visual effect
        if (pushEffect != null)
        {
            pushEffect.Play();
        }

        // Define the box center slightly in front of the player
        Vector3 boxCenter = transform.position + transform.forward * (boxSize.z / 2);

        // Find objects in the box area
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation, affectedLayers);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate push direction (only forward or sideways)
                Vector3 direction = (collider.transform.position - transform.position);
                direction.y = 0; // Ignore vertical force
                direction = direction.normalized;

                // Apply force in the determined direction
                rb.AddForce(direction * pushForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the push box for debugging
        Gizmos.color = Color.blue;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * (boxSize.z / 2), transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
