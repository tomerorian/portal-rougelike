using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MazeUnit
{
    Level level;

    protected override void Start()
    {
        level = Level.Instance;

        movement.MoveToMazePosition(Maze.Instance.GetStartPos());

        GetComponent<Health>().onDeath += OnDeath;
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
            return movement.AttemptMove(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return movement.AttemptMove(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return movement.AttemptMove(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return movement.AttemptMove(Vector2Int.right);
        }

        return false;
    }

    private void OnDeath()
    {
        GameSession.Instance.OnPlayerDeath();
    }
}
