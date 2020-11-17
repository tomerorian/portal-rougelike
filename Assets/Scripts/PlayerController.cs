using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] GridMovement movement = null;

    MazeGenerator mazeGen;

    private void Start()
    {
        mazeGen = FindObjectOfType<MazeGenerator>();
        rb.position = mazeGen.GetStartingPos();
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.Move(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.Move(Vector2Int.right);
        }
    }
}
