using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public GameObject endPointPrefab;

    public int OrbCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void CollectOrb()
    {
        OrbCount++;
        if (OrbCount == 5)
        {
            SpawnEndPoint();
        }
    }
    

    private void SpawnEndPoint()
    {
        if (OrbCount == 5)
        {
            //print("End point has appeared!");
            Vector3 endPosition = new Vector3(-2f, 0.54f, -1f); // Adjust position as needed
            Instantiate(endPointPrefab, endPosition, Quaternion.identity);
        }
    }
}
