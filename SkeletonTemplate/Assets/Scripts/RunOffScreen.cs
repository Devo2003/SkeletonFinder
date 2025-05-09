using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class RunOffScreen : MonoBehaviour
{
    public GameObject targetObject;
    public Vector3 direction = Vector3.right;
    public float speed = 5f;
    public float destroyAfterSeconds = 3f;
    private bool isRunning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRunning)
        {
            isRunning = true;
            StartCoroutine(MoveAndDestroy());
        }
    }

    private System.Collections.IEnumerator MoveAndDestroy()
    {
        float elapsedTime = 0f;
        while (elapsedTime < destroyAfterSeconds)
        {
            targetObject.transform.Translate(direction * speed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(targetObject);
    }
}
