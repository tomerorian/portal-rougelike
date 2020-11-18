using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMovement : GridMovement
{
    Maze maze = null;

    [Header("Config")]
    [SerializeField] LayerMask blockingLayers;


    private void Start()
    {
        maze = Maze.Instance;
    }

    public bool AttemptMove(Vector2Int direction)
    {
        Vector2Int targetPos = GetPosition() + direction;

        if (!maze.CanMoveTo(targetPos)) { return false; }

        if (IsSomethingBlocking(direction)) { return false; }

        Move(direction);

        return true;
    }

    private bool IsSomethingBlocking(Vector2Int direction)
    {
        // Assuming x and y size are identical
        float cellSize = grid.cellSize.x;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, cellSize, blockingLayers);

        return hits.Length > 0;
    }
}
