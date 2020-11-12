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

    Vector2Int startingPos;
    Cell[,] maze;

    private void Awake()
    {
        startingPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        maze = GenerateMaze(width, height, adjacentCellChange, startingPos);
    }

    private void Start()
    {
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

    public Vector2Int GetStartingPos()
    {
        return startingPos;
    }
}
