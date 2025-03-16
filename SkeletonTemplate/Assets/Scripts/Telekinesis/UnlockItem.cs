using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject telekinesisButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TelekinesisController.Instance != null)
            {
                TelekinesisController.Instance.UnlockTelekinesis();
                telekinesisButton.SetActive(true);
            }

            Destroy(gameObject); // Remove the item after collection

            if (particleEffect != null)
            {
                Instantiate(particleEffect, other.transform.position, Quaternion.identity);
            }
        }
    }
}
