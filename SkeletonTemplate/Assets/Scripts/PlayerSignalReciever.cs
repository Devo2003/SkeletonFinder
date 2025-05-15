using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSignalReciever : MonoBehaviour
{
    private PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void DisableMovement()
    {
        playerController.EnableMovement(false);
    }

    public void EnableMovement()
    {
        playerController.EnableMovement(true);
    }
}
