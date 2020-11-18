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
    [SerializeField] int pursuitSteps = 5;
    [SerializeField] float turnsPerMove = 2;

    MazeUnit target = null;
    int turnsSinceLastMove = -1;

    protected override TurnBasedUnit.BehaviourPriority GetPriority()
    {
        return TurnBasedUnit.BehaviourPriority.MOVEMENT;
    }

    public override IEnumerator TakeTurn()
    {
        DidAction = false;

        if (turnsSinceLastMove != -1)
        {
            turnsSinceLastMove++;
        }

        // Stil lock on targets even if we can't move right now
        if (!target)
        {
            target = FindVisibleEnemy();
        }

        if (turnsSinceLastMove != -1 && turnsSinceLastMove < turnsPerMove)
        {
            yield break;
        }

        if (target)
        {
            List<Vector2Int> path = AStar.FindPath(
                Maze.Instance, 
                unit.GetMovement().GetMazePos(),
                target.GetMovement().GetMazePos(), 
                Maze.Instance.GetValidNeighbourCoords(target.GetMovement().GetMazePos()));

            if (path.Count > pursuitSteps)
            {
                target = null;
                yield break;
            }

            if (path.Count >= 1 && unit.GetMovement().AttemptMoveToPos(path[1]))
            {
                turnsSinceLastMove = 0;
                DidAction = true;
            }
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
