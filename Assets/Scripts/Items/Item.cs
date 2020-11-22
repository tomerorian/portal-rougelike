using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] SpriteRenderer spriteRenderer = null;

    protected bool isActivated = false;

    public void Activate()
    {
        isActivated = true;
    }

    protected void Deactivate()
    {
        isActivated = false;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
