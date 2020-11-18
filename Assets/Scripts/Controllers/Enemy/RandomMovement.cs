using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : TurnBasedUnit
{
    public override IEnumerator TakeTurn()
    {
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0:
                {
                    movement.AttemptMove(this, Vector2Int.up);
                    break;
                }
            case 1:
                {
                    movement.AttemptMove(this, Vector2Int.down);
                    break;
                }
            case 2:
                {
                    movement.AttemptMove(this, Vector2Int.left);
                    break;
                }
            case 3:
                {
                    movement.AttemptMove(this, Vector2Int.right);
                    break;
                }
        }

        yield return null;
    }
}
