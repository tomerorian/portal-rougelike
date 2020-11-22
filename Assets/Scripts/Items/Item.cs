using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] SpriteRenderer spriteRenderer = null;

    protected bool isActivated = false;

    private void Awake()
    {
        enabled = false;
    }

    public void Activate()
    {
        enabled = true;
        isActivated = true;
    }

    protected void Deactivate()
    {
        enabled = false;
        isActivated = false;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
