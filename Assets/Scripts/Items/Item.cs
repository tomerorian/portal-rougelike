using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] SpriteRenderer spriteRenderer = null;

    protected bool isActivated = false;
    protected int inventoryIndex = -1;

    public void SetInventoryIndex(int index)
    {
        inventoryIndex = index;
    }

    private void Awake()
    {
        enabled = false;
    }

    public virtual void Activate()
    {
        enabled = true;
        isActivated = true;

        GameSession.Instance.playerController.enabled = false;
    }

    protected virtual void Deactivate()
    {
        enabled = false;
        isActivated = false;

        GameSession.Instance.playerController.enabled = true;
    }

    protected void Used()
    {
        Deactivate();

        Level.Instance.OnPlayerTookAction();
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
