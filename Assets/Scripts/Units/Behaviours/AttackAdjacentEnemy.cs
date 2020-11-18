using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAdjacentEnemy : TurnBasedBehaviour
{
    [Header("Config")]
    [SerializeField] LayerMask enemyLayers = 0;
    [SerializeField] int attackDamage = 1;

    protected override TurnBasedUnit.BehaviourPriority GetPriority()
    {
        return TurnBasedUnit.BehaviourPriority.ACTION;
    }

    public override IEnumerator TakeTurn()
    {
        MazeMovement unitMovement = unit.GetMovement();
        Maze maze = Maze.Instance;

        MazeUnit possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.up).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Attack(possibleTarget);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.right).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Attack(possibleTarget);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.down).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Attack(possibleTarget);
            yield return null;
        }

        possibleTarget = maze.GetCellData(unitMovement.GetMazePos() + Vector2Int.left).occupant;

        if (IsTargetValid(possibleTarget))
        {
            Attack(possibleTarget);
            yield return null;
        }
    }

    private bool IsTargetValid(MazeUnit target)
    {
        return target != null && (enemyLayers == (enemyLayers | (1 << target.gameObject.layer)));
    }

    private void Attack(MazeUnit possibleTarget)
    {
        Health targetHealth = possibleTarget.GetComponent<Health>();
        
        if (!targetHealth)
        {
            Debug.LogError(gameObject.name + " is trying to attack a target with no Health: " + possibleTarget.name);
            return;
        }

        targetHealth.TakeDamage(attackDamage);
    }
}
