using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBasedBehaviour : MonoBehaviour
{
    public abstract IEnumerator TakeTurn();
    protected abstract TurnBasedUnit.BehaviourPriority GetPriority();

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
