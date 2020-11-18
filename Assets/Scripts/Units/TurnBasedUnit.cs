using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedUnit : MazeUnit
{
    public enum BehaviourPriority
    {
        MOVEMENT,
        ACTION
    }

    Dictionary<BehaviourPriority, List<TurnBasedBehaviour>> behavioursByPriority = new Dictionary<BehaviourPriority, List<TurnBasedBehaviour>>();

    public IEnumerator TakeTurn()
    {
        foreach (BehaviourPriority enumValue in Enum.GetValues(typeof(BehaviourPriority)))
        {
            if (behavioursByPriority.ContainsKey(enumValue))
            {
                foreach (var behaviour in behavioursByPriority[enumValue])
                {
                    yield return behaviour.TakeTurn();
                }
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

    public void RegisterBehaviour(TurnBasedBehaviour behaviour, BehaviourPriority priority)
    {
        if (!behavioursByPriority.ContainsKey(priority))
        {
            behavioursByPriority.Add(priority, new List<TurnBasedBehaviour>());
        }

        behavioursByPriority[priority].Add(behaviour);
    }

    public void UnregisterBehaviour(TurnBasedBehaviour behaviour, BehaviourPriority priority)
    {
        behavioursByPriority[priority].Remove(behaviour);
    }
}
