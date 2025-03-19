using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorwayTeleport : MonoBehaviour
{
    public GameObject outsideCharacter; // Assign Player A (outside)
    public GameObject insideCharacter;  // Assign Player B (inside)

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == outsideCharacter)
        {
            SwitchCharacters(insideCharacter, outsideCharacter);
        }
        else if (other.gameObject == insideCharacter)
        {
            SwitchCharacters(outsideCharacter, insideCharacter);
        }
    }

    private void SwitchCharacters(GameObject newActive, GameObject oldActive)
    {
        oldActive.SetActive(false);  // Deactivate current character
        newActive.SetActive(true);   // Activate the new character
    }
}
