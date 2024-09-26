using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FOV : MonoBehaviour
{
    public float distance =10;
    public float angle = 30;
    public float height = 1.0f;
    public Color MeshColor = Color.red;
    public int RayCount = 30;
    public int RaySize = 30;

    public GameObject trianglePrefab;
    //private Material material;
    //private MeshRenderer meshRenderer;

    //public Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        //material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        //{
        //    UpdateShaderProperties(hit.point, hit.normal);
        //}
    }

    //private void UpdateShaderProperties(Vector3 hitPoint, Vector3 hitNormal)
    //{
    //    // Calculate angle and distance of detection
    //    float hitAngle = Vector3.Angle(transform.forward, hitPoint - transform.position);
    //    float hitDistance = Vector3.Distance(transform.position, hitPoint);

    //    // Set shader properties
    //    material.SetFloat("_ConeAngle", hitAngle);
    //    material.SetFloat("_ConeDistance", hitDistance);
    //}

    public void RayCast()
    {
        int PlayerLayer = 1 << LayerMask.NameToLayer("Player");

        // Direction of the ray 
        Vector3 forwardDirection = transform.forward;

        // Origin point of the ray 
        Vector3 rayOrigin = transform.position;

        // Cast rays for each vertex mesh
        for (int i = 0; i < RayCount; i++)
        {
            float currentAngle = Mathf.Lerp(-angle / 2f, angle / 2f, i / (float)(RayCount - 1));
            Vector3 rayDirection = Quaternion.Euler(0, currentAngle, 0) * forwardDirection * RaySize;

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, distance, PlayerLayer))
            {
                Debug.DrawLine(rayOrigin, hit.point, Color.blue); // display the blue rays where the capsule was spotted from
                Debug.Log("Hit player: " + hit.collider.gameObject.name);
                //Player Found
                SceneManager.LoadScene("Finished");
                 // Instantiate the triangle GameObject at the hit point
                GameObject triangle = Instantiate(trianglePrefab, hit.point, Quaternion.identity);

                // Calculate rotation to align triangle with ray direction
                Quaternion rotation = Quaternion.LookRotation(rayDirection, Vector3.up);
                triangle.transform.rotation = rotation;
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red); // display the search rays
            }
        }
    }
}
