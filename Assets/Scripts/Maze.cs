using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static MazeGeneration;

public class Maze : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Tilemap layoutTileMap = null;
    [SerializeField] TileBase tile = null;

    [Header("Config")]
    [SerializeField] int width = 50;
    [SerializeField] int height = 50;
    [SerializeField] float adjacentCellChance = 0.06f;

    Vector2Int startingPos;
    Cell[,] maze;

    private void Awake()
    {
        //Random.InitState(0);
        startingPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        maze = GenerateMaze(width, height, adjacentCellChance, startingPos);
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

    public Vector2 MazeToWorldPos(Vector2Int pos)
    {
        return layoutTileMap.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }

    public bool CanMoveTo(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height)
        { 
            return false;
        }

        return maze[pos.x, pos.y].isPath;
    }
}
