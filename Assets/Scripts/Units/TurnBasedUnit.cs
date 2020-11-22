using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedUnit : MazeUnit
{
    public bool IsActive = true;
    public bool FinishedTurn { get; private set; }

    PriorityQueue<TurnBasedBehaviour> behaviours = new PriorityQueue<TurnBasedBehaviour>();

    public IEnumerator TakeTurn()
    {
        if (!IsActive)
        {
            FinishedTurn = true;
            yield break;
        }

        FinishedTurn = false;

        foreach (TurnBasedBehaviour behaviour in behaviours.Entires)
        {
            yield return behaviour.TakeTurn();

            if (behaviour.DidAction)
            {
                FinishedTurn = true;
                yield break;
            }
        }

        FinishedTurn = true;
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
