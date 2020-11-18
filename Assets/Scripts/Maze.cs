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
    [SerializeField] GameObject entrancePrefab = null;
    [SerializeField] GameObject exitPrefab = null;

    [Header("Config")]
    [SerializeField] int width = 50;
    [SerializeField] int height = 50;
    [Range(0f, 0.1f)]
    [SerializeField] float adjacentCellChance = 0.06f;

    [Header("Debug")]
    [Tooltip("Will set a forced random seed if set to anything but 0")]
    [SerializeField] int forcedMazeSeed = 0;

    Vector2Int startPos;
    Cell[,] maze;
    MazePopulation mazePopulation;

    private void Awake()
    {
        if (forcedMazeSeed != 0)
        {
            Random.InitState(forcedMazeSeed);
        }
        
        startPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        maze = GenerateMaze(width, height, adjacentCellChance, startPos);

        mazePopulation = new MazePopulation(maze, startPos);
    }

    private void Start()
    {
        mazePopulation.PopulateMaze();
        SetTiles();
        CreateEntrance();
        CreateExit();
    }

    private void SetTiles()
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

    private void CreateEntrance()
    {
        Instantiate(entrancePrefab, MazeToWorldPos(startPos), Quaternion.identity);
    }

    private void CreateExit()
    {
        Instantiate(exitPrefab, MazeToWorldPos(mazePopulation.ExitPos), Quaternion.identity);
    }

    public Vector2Int GetStartPos()
    {
        return startPos;
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
