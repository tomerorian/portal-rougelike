using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected bool isActivated = false;

    public void Activate()
    {
        isActivated = true;
    }

    protected void Deactivate()
    {
        isActivated = false;
    }
}
