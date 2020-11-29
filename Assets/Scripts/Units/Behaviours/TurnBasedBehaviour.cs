using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBasedBehaviour : MonoBehaviour, IComparable<TurnBasedBehaviour>
{
    public abstract IEnumerator TakeTurn();

    [SerializeField] protected int priority = 100;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public bool DidAction { get; protected set; }

    protected TurnBasedUnit unit;

    private void Awake()
    {
        unit = GetComponent<TurnBasedUnit>();
        unit.RegisterBehaviour(this);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        unit.UnregisterBehaviour(this);
    }

    public int CompareTo(TurnBasedBehaviour other)
    {
        return this.priority - other.priority;
    }

    protected bool ShouldAnimate()
    {
        return spriteRenderer.isVisible;
    }
}
