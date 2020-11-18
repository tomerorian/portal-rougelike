using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    [SerializeField] protected MazeMovement movement = null;

    Vector2Int mazePos;

    private void Start()
    {
        movement.MoveToMazePosition(this, Maze.Instance.WorldToMazePos(transform.position));
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
