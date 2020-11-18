using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] protected Grid grid = null;
    [SerializeField] protected new Rigidbody2D rigidbody = null;

    // TODO: Remove? (not using this for now)
    private void AdjustToGrid()
    {
        transform.position = grid.GetCellCenterWorld(grid.WorldToCell(transform.position));
    }

    public void Move(Vector2Int direction)
    {
        rigidbody.MovePosition(new Vector2(transform.position.x + direction.x, transform.position.y + direction.y));
    }

    public Vector2Int GetPosition()
    {
        Vector3Int position = grid.WorldToCell(transform.position);
        return new Vector2Int(position.x, position.y);
    }
}
