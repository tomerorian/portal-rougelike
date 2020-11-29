using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToVisibleEnemy : TurnBasedBehaviour
{
    [Header("Config")]
    [SerializeField] float visionRange = 5f;
    [SerializeField] LayerMask enemyLayers = 0;
    [SerializeField] LayerMask visionBlockingLayers = 0;
    [SerializeField] int pursuitSteps = 5;
    [SerializeField] float turnsPerMove = 2;

    MazeUnit target = null;
    int turnsSinceLastMove = -1;

    public override IEnumerator TakeTurn()
    {
        DidAction = false;

        if (turnsSinceLastMove != -1)
        {
            turnsSinceLastMove++;
        }

        // Still lock on targets even if we can't move right now
        if (!target)
        {
            target = VisionUtils.FindVisibleEnemy(transform, visionRange, enemyLayers, visionBlockingLayers);
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
}
