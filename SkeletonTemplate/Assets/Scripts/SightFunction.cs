using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightFunction : MonoBehaviour
{
    public float maxRaycastDistance = 10f;

    private Mesh mesh;
    private MeshFilter meshFilter;

    void Start()
    {
        // Create a new mesh
        GameObject meshObject = new GameObject("RaycastMesh");
        meshObject.transform.parent = transform;
        meshFilter = meshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();

        // Set material (create a material if needed)
        meshRenderer.material = new Material(Shader.Find("Standard"));

        // Create an initial mesh (can be updated later)
        mesh = new Mesh();
        mesh.vertices = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero }; // Initialize vertices
        mesh.triangles = new int[] { 0, 1, 2 }; // Define triangles
        mesh.normals = new Vector3[] { Vector3.up, Vector3.up, Vector3.up }; // Set normals (optional)
        meshFilter.mesh = mesh;
    }

    void Update()
    {
        // Calculate the raycast origin and direction
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;

        // Perform raycast
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, maxRaycastDistance))
        {
            // Calculate vertices based on raycast information
            Vector3[] vertices = new Vector3[3];
            vertices[0] = transform.position; // Start point (center of triangle)
            vertices[1] = hit.point; // End point of raycast
            vertices[2] = Vector3.Lerp(transform.position, hit.point, 0.5f); // Midpoint between start and end

            // Update mesh vertices
            mesh.vertices = vertices;

            // Recalculate normals and bounds for proper rendering
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
        else
        {
            // Handle no hit case (set mesh size to zero or default size)
            // For example, set all vertices to zero to make the mesh invisible
            mesh.vertices = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero };
        }
    }


}
