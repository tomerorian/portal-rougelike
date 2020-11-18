using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToVisibleEnemy : TurnBasedBehaviour
{
    [Header("Config")]
    [SerializeField] float visionRange = 5f;
    [SerializeField] LayerMask enemyLayers = 0;
    [SerializeField] LayerMask visiionBlockingLayers = 0;

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
            print("Found enemy! " + enemy.name);
            DidAction = true;
        }

        yield break;
    }

    private MazeUnit FindVisibleEnemy()
    {
        // Find all enemies within range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, visionRange, Vector2.zero, 0, enemyLayers);

        if (hits.Length == 0) { return null; }

        // Find first visible target
        foreach (RaycastHit2D hit in hits)
        {
            RaycastHit2D visibleHit = Physics2D.Raycast(transform.position, hit.transform.position - transform.position, float.MaxValue, enemyLayers | visiionBlockingLayers);

            if (hit.collider == visibleHit.collider)
            {
                return hit.collider.GetComponent<MazeUnit>();
            }
        }

        return null;
    }
}
