using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUnit : MonoBehaviour
{
    [SerializeField] protected MazeMovement movement = null;

    protected virtual void Awake()
    {
        movement.Init(this);
    }

    private void Start()
    {
        movement.MoveToMazePosition(Maze.Instance.WorldToMazePos(transform.position));
    }

    public MazeMovement GetMovement()
    {
        return movement;
    }
}
