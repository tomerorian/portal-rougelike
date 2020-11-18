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

        MoveToMazePosition(unit, targetPos);

        return true;
    }

    public void MoveToMazePosition(MazeUnit unit, Vector2Int pos)
    {
        maze.MoveUnitTo(unit, pos);
        unit.SetMazePos(pos);
        SetPosition(pos);
    }
}
