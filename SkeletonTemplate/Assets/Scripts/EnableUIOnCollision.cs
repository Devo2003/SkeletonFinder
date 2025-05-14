using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUIOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject uiParent; // The object to enable
    [SerializeField] private string targetTag = "Player"; // The tag of the object that triggers the collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log($"Collision detected with: {other.gameObject.name}");

            if (uiParent != null)
            {                
                Debug.Log($"Enabling UI Parent: {uiParent.name}");
                uiParent.SetActive(true);

                foreach (Transform child in uiParent.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
