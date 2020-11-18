using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    [SerializeField] protected MazeMovement movement = null;

    private void Start()
    {
        movement.MoveToMazePosition(this, Maze.Instance.WorldToMazePos(transform.position));
    }

    public MazeMovement GetMovement()
    {
        return movement;
    }
}
