using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tri : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;

    void Start()
    {
        Mesh mesh = new Mesh();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Calculate the vertices of the triangle based on the given parameters
        Vector3[] vertices = CalculateTriangleVertices(distance, angle, height);

        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2 };

        meshFilter.mesh = mesh;
    }

    // Method to calculate the vertices of the triangle
    public Vector3[] CalculateTriangleVertices(float distance, float angle, float height)
    {
        // Calculate the half base of the triangle
        float halfBase = distance / 2f;

        // Convert angle from degrees to radians
        float radians = angle * Mathf.Deg2Rad;

        // Calculate the height of the triangle
        float opposite = Mathf.Sin(radians) * height;

        // Calculate the coordinates of the vertices
        Vector3 vertex0 = new Vector3(0, 0, 0);
        Vector3 vertex1 = new Vector3(distance, 0, 0);
        Vector3 vertex2 = new Vector3(halfBase, opposite, 0);

        return new Vector3[] { vertex0, vertex1, vertex2 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
