using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MazeUnit
{
    Level level;

    private void Start()
    {
        level = Level.Instance;

        movement.SetPosition(Maze.Instance.GetStartPos());
    }

    private void Update()
    {
        if (!level.IsPlayerTurn()) { return; }

        if (HandleMovementInput()) 
        {
            level.OnPlayerTookAction();
            return;
        }
    }

    private bool HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return movement.AttemptMove(this, Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return movement.AttemptMove(this, Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return movement.AttemptMove(this, Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return movement.AttemptMove(this, Vector2Int.right);
        }

        return false;
    }
}
