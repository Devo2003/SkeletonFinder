using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFunction : MonoBehaviour
{
    public bool hasArm = false;  // Tracks if the player has collected the arm
    public GameObject rockPrefab; // Rock prefab

    private void OnTriggerEnter(Collider other)
    {
        // Collect the arm
        if (other.gameObject.CompareTag("Arm"))
        {
            PickUpArm();
            Destroy(other.gameObject);  // Remove the arm collectible
        }
    }

    private void Update()
    {
        // Allow throwing only if the arm has been collected
        if (hasArm && Input.GetButtonDown("Fire1"))
        {
            ThrowRock();
        }
    }

    private void PickUpArm()
    {
        hasArm = true;  // Enable rock throwing
    }
    public float throwForce;
    private void ThrowRock()
    {
        // Get the direction to throw based on the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 throwDirection = (hit.point - transform.position).normalized; // Use the player's position
            throwForce = 25f;  // Adjust force as needed

            // Instantiate the rock at the player's position
            GameObject rock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = rock.GetComponent<Rigidbody>(); // Ensure the rock has a Rigidbody
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

    }

}
