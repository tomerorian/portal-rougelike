using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBasedBehaviour : MonoBehaviour
{
    protected abstract TurnBasedUnit.BehaviourPriority GetPriority();
    public abstract IEnumerator TakeTurn();

    public bool DidAction { get; protected set; }

    protected TurnBasedUnit unit;

    private void Awake()
    {
        unit = GetComponent<TurnBasedUnit>();
        unit.RegisterBehaviour(this, GetPriority());
    }

    private void OnDestroy()
    {
        unit.UnregisterBehaviour(this, GetPriority());
    }
}
