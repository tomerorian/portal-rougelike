using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] MazeMovement movement = null;

    private void Start()
    {
        rb.position = Maze.Instance.MazeToWorldPos(Maze.Instance.GetStartPos());
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.AttemptMove(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.AttemptMove(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.AttemptMove(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.AttemptMove(Vector2Int.right);
        }
    }
}
