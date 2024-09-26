using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform Player;
    //public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 desiredposition = Player.position + offset;
        //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, desiredposition, smoothSpeed);
        transform.position = desiredposition;
    }
}
