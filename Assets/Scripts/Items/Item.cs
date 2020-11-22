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

        GameSession.Instance.player.GetComponent<PlayerController>().enabled = false;
    }

    protected void Deactivate()
    {
        enabled = false;
        isActivated = false;

        GameSession.Instance.player.GetComponent<PlayerController>().enabled = true;
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
