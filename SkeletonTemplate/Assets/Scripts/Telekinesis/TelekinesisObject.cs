using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisObject : MonoBehaviour
{
    //private bool isMoving = false;

    //public void StartMovement(Vector3 targetPosition, float speed, float duration)
    //{
    //    if (!isMoving)
    //    {
    //        StartCoroutine(MoveToPosition(targetPosition, speed, duration));
    //    }
    //}

    //private IEnumerator MoveToPosition(Vector3 targetPosition, float speed, float duration)
    //{
    //    isMoving = true;
    //    Vector3 startPosition = transform.position;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
    //        elapsedTime += Time.deltaTime * speed;
    //        yield return null;
    //    }

    //    transform.position = targetPosition; // Ensure it reaches the final position
    //    isMoving = false;
    //}

    private bool isMoving = false;
    private bool hasMoved = false; // Prevents multiple moves

    public void StartMovement(Vector3 targetPosition, float speed, float duration)
    {
        if (!isMoving && !hasMoved) // Ensures it only moves once
        {
            hasMoved = true; // Mark it as moved so it won't move again
            StartCoroutine(MoveToPosition(targetPosition, speed, duration));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, float speed, float duration)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        transform.position = targetPosition; // Ensure it reaches the final position
        isMoving = false;
    }
}
