using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static MazeGeneration;

public class MazeGenerator : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Tilemap layoutTileMap = null;
    [SerializeField] TileBase tile = null;

    [Header("Config")]
    [SerializeField] int width = 50;
    [SerializeField] int height = 50;
    [SerializeField] float adjacentCellChange = 0.06f;

    void Start()
    {
        Cell[,] maze = GenerateMaze(width, height, adjacentCellChange);

        Vector3Int pos = new Vector3Int(0, 0, 0);

        foreach (Cell cell in maze)
        {
            if (cell.isPath)
            {
                pos.x = cell.x;
                pos.y = cell.y;
                layoutTileMap.SetTile(pos, tile);
            }
        }
    }
}
