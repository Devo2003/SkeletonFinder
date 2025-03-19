using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FOV : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color MeshColor = Color.red;
    public int RayCount = 30;
    public int RaySize = 30;

    public GameObject trianglePrefab;
    private GameObject instantiatedTriangle = null; // Store reference to the instantiated triangle
    private bool playerDetected = false; // Flag to check if player is detected
    public float detectionTime = 2f; // The time required to detect the player (2 seconds)
    private float currentDetectionTime = 0f; // Timer for detection

    //public MainMenu menu;

    private Transform player; // Reference to player

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assuming the player has the "Player" tag
    }

    void Update()
    {
        RayCast();
    }

    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }

    public void RayCast()
    {
        int PlayerLayer = 1 << LayerMask.NameToLayer("Player");

        Vector3 forwardDirection = transform.forward;
        Vector3 rayOrigin = transform.position;

        bool playerInCone = false; // Track if the player is within the detection cone

        // Cast rays for each vertex mesh
        for (int i = 0; i < RayCount; i++)
        {
            float currentAngle = Mathf.Lerp(-angle / 2f, angle / 2f, i / (float)(RayCount - 1));
            Vector3 rayDirection = Quaternion.Euler(0, currentAngle, 0) * forwardDirection;

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, distance, PlayerLayer))
            {
                Debug.DrawLine(rayOrigin, hit.point, Color.blue); // Display the blue rays where the player was spotted
                //Debug.Log("Player in cone");

                playerInCone = true; // Set to true when the player is inside the detection cone

                // Instantiate the triangle if it hasn't been instantiated yet
                if (instantiatedTriangle == null)
                {
                    instantiatedTriangle = Instantiate(trianglePrefab, hit.point, Quaternion.identity);
                    Quaternion rotation = Quaternion.LookRotation(rayDirection, Vector3.up);
                    instantiatedTriangle.transform.rotation = rotation;
                }
                else
                {
                    // If the triangle is already instantiated, just update its position and rotation
                    instantiatedTriangle.transform.position = hit.point;
                    Quaternion rotation = Quaternion.LookRotation(rayDirection, Vector3.up);
                    instantiatedTriangle.transform.rotation = rotation;
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red); // Display the search rays
            }
        }

        // If the player is in the cone, increase the detection timer
        if (playerInCone)
        {
            currentDetectionTime += Time.deltaTime;

            if (currentDetectionTime >= detectionTime)
            {
                playerDetected = true; // Player is detected after 2 seconds
                Debug.Log("Player detected");


                // Add detection logic here, e.g., load a new scene or trigger an alarm
                //this.menu.ShowGameOver();
                //Retry();
                //SceneManager.LoadScene("Game Over");
            }
        }
        else
        {
            // If the player leaves the cone, reset the detection timer
            currentDetectionTime = 0f;

            // Destroy the triangle if the player is no longer in the cone
            if (instantiatedTriangle != null)
            {
                Destroy(instantiatedTriangle);
                instantiatedTriangle = null;
            }
        }
    }
}
