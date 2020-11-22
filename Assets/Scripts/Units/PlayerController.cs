﻿using UnityEngine;

public class PlayerController : MazeUnit
{
    Level level;

    protected override void Start()
    {
        level = Level.Instance;

        movement.MoveToMazePosition(Maze.Instance.GetStartPos());

        GetComponent<Health>().onDeath += OnDeath;
    }

    private void Update()
    {
        if (!level.IsPlayerTurn()) { return; }

        if (HandleMovementInput()) 
        {
            level.OnPlayerTookAction();
            return;
        }

        if (HandlePickup())
        {
            level.OnPlayerTookAction();
            return;
        }

        HandleItemUse();
    }

    private bool HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return movement.AttemptMove(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return movement.AttemptMove(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return movement.AttemptMove(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return movement.AttemptMove(Vector2Int.right);
        }

        return false;
    }

    private bool HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameSession.Instance.playerInventory.IsFull()) { return false; }

            Vector2Int pos = movement.GetMazePos();
            Item item = Maze.Instance.GetItem(pos);

            if (item == null) { return false; }

            if (GameSession.Instance.playerInventory.AttemptAddItem(item))
            {
                Maze.Instance.RemoveItemAt(pos);
                return true;
            }

            return false;
        }

        return false;
    }

    private void HandleItemUse()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UseItem(4);
        }
    }

    private void UseItem(int index)
    {
        Item item = GameSession.Instance.playerInventory.GetItemAt(index);

        if (item)
        {
            item.Activate();
        }
    }

    private void OnDeath()
    {
        GameSession.Instance.OnPlayerDeath();
    }
}
