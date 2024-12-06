using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public float pushRadius = 5f;  // Radius of the force push
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

        // Find objects in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius, affectedLayers);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate direction and apply force
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rb.AddForce(direction * pushForce, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the push radius for debugging
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pushRadius);
    }
}
