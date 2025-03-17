using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TelekinesisObject : MonoBehaviour
{
    public abstract void ActivateTelekinesis(); // Each object implements its movement

    public virtual void HighlightObject(bool highlight)
    {

    }
}
