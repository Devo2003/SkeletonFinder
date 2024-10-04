using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoFunction : MonoBehaviour
{
    public bool hasTorso = false;
    //public List<GameObject> inventoryItems = new List<GameObject>();
    //store item names instead of the actual objects because it get destoyed when collected
    public List<string> inventoryItems = new List<string>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Torso"))
        {
            //equip the torso
            PickUpTorso();
            //need to have anaimation to attach torso here instead of destroying object
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            if (hasTorso)
            {
                PickUpItem(other.gameObject);
            }
            else
            {
                Debug.Log("Need torso to pick up Key!");
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        inventoryItems.Add(item.name);
        Debug.Log(item.name + "Picked up");
        Destroy(item);
    }

    private void PickUpTorso()
    {
        hasTorso = true;
        Debug.Log("Has equiped Torso!");
    }
}
