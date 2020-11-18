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

    public void SetWorldPositionByGrid(Vector2Int position)
    {
        transform.position = grid.GetCellCenterWorld(new Vector3Int(position.x, position.y, 0));
    }

    public Vector2Int GetGridPosition()
    {
        Vector3Int position = grid.WorldToCell(transform.position);
        return new Vector2Int(position.x, position.y);
    }
}
