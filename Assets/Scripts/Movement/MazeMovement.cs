using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMovement : GridMovement
{
    Vector2Int mazePos;
    MazeUnit unit;

    public void Init(MazeUnit unit)
    {
        this.unit = unit;
    }

    public bool AttemptMove(Vector2Int direction)
    {
        return AttemptMoveToPos(mazePos + direction);
    }

    public bool AttemptMoveToPos(Vector2Int targetPos)
    {
        if (!Maze.Instance.CanMoveTo(targetPos)) { return false; }

        MoveToMazePosition(targetPos);

        return true;
    }

    public void MoveToMazePosition(Vector2Int pos)
    {
        Maze.Instance.MoveUnitTo(unit, pos);
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
