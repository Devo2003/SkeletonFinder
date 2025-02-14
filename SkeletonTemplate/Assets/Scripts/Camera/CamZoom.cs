using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
    public float zoomSpeed = 5f; // Zoom sensitivity
    public float minZoom = 5f;   // Closest zoom limit
    public float maxZoom = 20f;  // Farthest zoom limit

    private float currentZoom;

    void Start()
    {
        currentZoom = Vector3.Distance(transform.position, transform.parent.position);
    }

    void Update()
    {
        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            currentZoom -= scrollInput * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            // Move camera along forward direction while maintaining rotation
            transform.position = transform.parent.position - transform.forward * currentZoom;
        }
    }
}
