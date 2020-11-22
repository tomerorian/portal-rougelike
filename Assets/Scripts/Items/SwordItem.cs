using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : Item
{
    [SerializeField] GameObject attackIndicatorPrefab = null;

    GameObject attackIndicator = null;
    Vector2Int attackPos = Vector2Int.zero;

    public override void Activate()
    {
        base.Activate();

        attackIndicator = Instantiate(attackIndicatorPrefab, GameSession.Instance.player.transform);
        PositionIndicator(Vector2Int.up);
    }

    protected override void Deactivate()
    {
        base.Deactivate();

        Destroy(attackIndicator);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PositionIndicator(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PositionIndicator(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PositionIndicator(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PositionIndicator(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Deactivate();
        }
    }

    private void PositionIndicator(Vector2Int direction)
    {
        Vector2Int playerPos = GameSession.Instance.playerController.GetMovement().GetMazePos();
        attackPos = playerPos + direction;

        attackIndicator.transform.position = Maze.Instance.MazeToWorldPos(attackPos);
    }
}
