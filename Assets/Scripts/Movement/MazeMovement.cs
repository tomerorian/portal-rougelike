using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMovement : GridMovement
{
    Maze maze = null;

    [Header("Config")]
    [SerializeField] LayerMask blockingLayers = 0;

    Vector2Int mazePos;
    MazeUnit unit;

    private void Start()
    {
        maze = Maze.Instance;
    }

    public void Init(MazeUnit unit)
    {
        this.unit = unit;
    }

    public bool AttemptMove(Vector2Int direction)
    {
        Vector2Int targetPos = mazePos + direction;

        if (!maze.CanMoveTo(targetPos)) { return false; }

        MoveToMazePosition(targetPos);

        return true;
    }

    public void MoveToMazePosition(Vector2Int pos)
    {
        maze.MoveUnitTo(unit, pos);
        mazePos = pos;
        SetWorldPositionByGrid(pos);
    }

    public Vector2Int GetMazePos()
    {
        return mazePos;
    }

    public void SetMazePos(Vector2Int pos)
    {
        mazePos = pos;
    }
}
