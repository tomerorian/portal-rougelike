using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedUnit : MazeUnit
{
    PriorityQueue<TurnBasedBehaviour> behaviours = new PriorityQueue<TurnBasedBehaviour>();

    public IEnumerator TakeTurn()
    {
        foreach (TurnBasedBehaviour behaviour in behaviours.Entires)
        {
            yield return behaviour.TakeTurn();

            if (behaviour.DidAction)
            {
                yield break;
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        Level.Instance.AddUnit(this);
    }

    protected virtual void OnDestroy()
    {
        Level.Instance.RemoveUnit(this);
    }

    public void RegisterBehaviour(TurnBasedBehaviour behaviour)
    {
        behaviours.Enqueue(behaviour);
    }

    public void UnregisterBehaviour(TurnBasedBehaviour behaviour)
    {
        behaviours.Entires.Remove(behaviour);
    }
}
