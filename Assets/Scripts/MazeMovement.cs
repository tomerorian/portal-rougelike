using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMovement : GridMovement
{
    Maze maze = null;

    private void Start()
    {
        maze = Maze.Instance;
    }

    public bool AttemptMove(Vector2Int direction)
    {
        if (maze.CanMoveTo(GetPosition() + direction))
        {
            Move(direction);
            return true;
        }

        return false;
    }
}
