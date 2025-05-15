using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrevorBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingHeight = 0.1f;     // How high the object bobs
    public float bobbingSpeed = 2f;        // How fast it bobs
    public Transform targetObject;         // Reference to the witch or the object the sack is attached to

    private Vector3 initialPosition;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Target object not set. Defaulting to parent object.");
            targetObject = transform.parent;
        }

        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (targetObject != null)
        {
            float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
            transform.localPosition = initialPosition + new Vector3(0, bobbingOffset, 0);
        }
    }
}
