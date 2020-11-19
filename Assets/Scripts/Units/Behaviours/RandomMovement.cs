using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : TurnBasedBehaviour
{
    public override IEnumerator TakeTurn()
    {
        int randomDirection = Random.Range(0, 4);

        switch (randomDirection)
        {
            case 0:
                {
                    DidAction = unit.GetMovement().AttemptMove(Vector2Int.up);
                    break;
                }
            case 1:
                {
                    DidAction = unit.GetMovement().AttemptMove(Vector2Int.down);
                    break;
                }
            case 2:
                {
                    DidAction = unit.GetMovement().AttemptMove(Vector2Int.left);
                    break;
                }
            case 3:
                {
                    DidAction = unit.GetMovement().AttemptMove(Vector2Int.right);
                    break;
                }
        }

        yield break;
    }
}
