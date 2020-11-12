using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Rigidbody2D rb = null;

    [Header("Config")]
    [SerializeField] float walkSpeed = 5f;

    Vector2 movementVector = new Vector2();
    float horizontalInput;
    float verticalInput;
    float horiztonalInputTime = 0;
    float verticalInputTime = 0;

    MazeGenerator mazeGen;

    private void Start()
    {
        mazeGen = FindObjectOfType<MazeGenerator>();
        rb.position = mazeGen.GetStartingPos();
    }

    private void Update()
    {
        HandleMovementInput();
        MazeGeneration.Cell cell = mazeGen.GetMazeCellByWorldPos(rb.position);
        print(cell.distanceFromStart);
    }

    private void HandleMovementInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0 && horiztonalInputTime == 0)
        {
            horiztonalInputTime = Time.time;
        }
        else if (horizontalInput == 0)
        {
            horiztonalInputTime = 0;
        }

        if (Mathf.Abs(verticalInput) > 0 && verticalInputTime == 0)
        {
            verticalInputTime = Time.time;
        }
        else if (verticalInput == 0)
        {
            verticalInputTime = 0;
        }
    }

    private void FixedUpdate()
    {
        bool hasHorizontalInput = Mathf.Abs(horizontalInput) > 0;
        bool hasVerticalInput = Mathf.Abs(verticalInput) > 0;

        if (hasHorizontalInput && hasVerticalInput)
        {
            if (horiztonalInputTime > verticalInputTime)
            {
                MoveHorizontally();
            }
            else
            {
                MoveVertically();
            }
        }
        else if (hasHorizontalInput)
        {
            MoveHorizontally();
        }
        else if (hasVerticalInput)
        {
            MoveVertically();
        }
    }

    private void MoveVertically()
    {
        movementVector.x = 0;
        movementVector.y = verticalInput;

        rb.MovePosition(rb.position + (movementVector * walkSpeed * Time.fixedDeltaTime));
    }

    private void MoveHorizontally()
    {
        movementVector.x = horizontalInput;
        movementVector.y = 0;

        rb.MovePosition(rb.position + (movementVector * walkSpeed * Time.fixedDeltaTime));
    }
}
