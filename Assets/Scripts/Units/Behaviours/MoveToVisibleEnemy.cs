using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToVisibleEnemy : TurnBasedBehaviour
{
    [Header("Config")]
    [SerializeField] float visionRange = 5f;

    protected override TurnBasedUnit.BehaviourPriority GetPriority()
    {
        return TurnBasedUnit.BehaviourPriority.MOVEMENT;
    }

    public override IEnumerator TakeTurn()
    {
        DidAction = false;

        MazeUnit enemy = FindVisibleEnemy();

        if (enemy)
        {
            // TODO: Attack enemy
            yield break;
        }
    }

    private MazeUnit FindVisibleEnemy()
    {
        return null;
    }
}
