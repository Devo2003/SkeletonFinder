using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public float rotationSpeed = 100.0f;
    public float LeftRotationLimit = -45.0f;
    public float RightRotationLimit = 45.0f;
    public float PauseCamera = 1.0f;

    private Quaternion LeftDirectionLimit;
    private Quaternion RightDirectionLimit;
    private Quaternion OGRotation;

    private bool rotationRight = true;

    // Start is called before the first frame update
    void Start()
    {
        OGRotation = transform.rotation;

        LeftDirectionLimit = OGRotation * Quaternion.Euler(0, LeftRotationLimit, 0);
        RightDirectionLimit = OGRotation * Quaternion.Euler(0, RightRotationLimit, 0);

        StartCoroutine(RotateCamera());
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RotateCamera()
    {
        while (true)
        {
            Quaternion targetRotation = rotationRight ? RightDirectionLimit : LeftDirectionLimit;

            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(PauseCamera);

            rotationRight = !rotationRight;

        }
    }
}
