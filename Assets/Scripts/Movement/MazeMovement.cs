using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMovement : GridMovement
{
    Maze maze = null;

    [Header("Config")]
    [SerializeField] LayerMask blockingLayers = 0;


    private void Start()
    {
        maze = Maze.Instance;
    }

    public bool AttemptMove(MazeUnit unit, Vector2Int direction)
    {
        Vector2Int targetPos = GetPosition() + direction;

        if (!maze.CanMoveTo(targetPos)) { return false; }

        if (IsSomethingBlocking(direction)) { return false; }

        SetMazePosition(unit, targetPos);

        return true;
    }

    public void SetMazePosition(MazeUnit unit, Vector2Int pos)
    {
        maze.MoveUnitTo(unit, pos);
        unit.SetMazePos(pos);
        SetPosition(pos);
    }

    private bool IsSomethingBlocking(Vector2Int direction)
    {
        // Assuming x and y size are identical
        float cellSize = grid.cellSize.x;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, cellSize, blockingLayers);

        return hits.Length > 0;
    }
}
