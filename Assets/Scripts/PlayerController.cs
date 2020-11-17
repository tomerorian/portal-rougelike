using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] GridMovement movement = null;

    Maze maze;

    private void Start()
    {
        maze = FindObjectOfType<Maze>();
        rb.position = maze.MazeToWorldPos(maze.GetStartingPos());
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AttemptMove(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AttemptMove(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AttemptMove(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AttemptMove(Vector2Int.right);
        }
    }

    private void AttemptMove(Vector2Int direction)
    {
        if (maze.CanMoveTo(movement.GetPosition() + direction))
        {
            movement.Move(direction);
        }
    }
}
