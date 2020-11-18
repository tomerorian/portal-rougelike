using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBasedUnit : MazeUnit
{
    public abstract IEnumerator TakeTurn();

    protected override void Start()
    {
        base.Start();

        Level.Instance.AddUnit(this);
    }

    protected virtual void OnDestroy()
    {
        Level.Instance.RemoveUnit(this);
    }
}
