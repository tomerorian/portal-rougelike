using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAdjacentEnemy : TurnBasedBehaviour
{
    [Header("Config")]
    [SerializeField] LayerMask enemyLayers = 0;

    public override IEnumerator TakeTurn()
    {
        MazeMovement unitMovement = unit.GetMovement();
        Maze maze = Maze.Instance;

        MazeUnit possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.up).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Debug.Log("Attacking: " + possibleTarget.name);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.right).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Debug.Log("Attacking: " + possibleTarget.name);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.down).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Debug.Log("Attacking: " + possibleTarget.name);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.left).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Debug.Log("Attacking: " + possibleTarget.name);
            yield return null;
        }
    }

    private bool IsTargetValid(MazeUnit target)
    {
        return target != null && (enemyLayers == (enemyLayers | (1 << target.gameObject.layer)));
    }

    protected override TurnBasedUnit.BehaviourPriority GetPriority()
    {
        return TurnBasedUnit.BehaviourPriority.ACTION;
    }
}
