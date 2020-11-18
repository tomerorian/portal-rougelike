using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBasedUnit : MonoBehaviour
{
    public abstract IEnumerator TakeTurn();

    protected virtual void Start()
    {
        Level.Instance.AddUnit(this);
    }

    protected virtual void OnDestroy()
    {
        Level.Instance.RemoveUnit(this);
    }
}
