using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPlate : MonoBehaviour
{
    public AudioSource soundSource;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        soundSource.Play();
    }


}
