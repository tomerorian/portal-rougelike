﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static MazeGeneration;

public class Maze : MonoBehaviour
{
    public static Maze Instance { get; private set; }

    [Header("Refs")]
    [SerializeField] Tilemap pathTilemap = null;
    [SerializeField] TileBase pathTile = null;
    [SerializeField] Tilemap wallTilemap = null;
    [SerializeField] TileBase wallTile = null;
    [SerializeField] GameObject entrancePrefab = null;
    [SerializeField] GameObject exitPrefab = null;

    [Header("Config")]
    [SerializeField] int width = 50;
    [SerializeField] int height = 50;
    [Range(0f, 0.2f)]
    [SerializeField] float adjacentCellChance = 0.06f;
    [SerializeField] int borderWallWidth = 5;

    [Header("Debug")]
    [Tooltip("Will set a forced random seed if set to anything but 0")]
    [SerializeField] int forcedMazeSeed = 0;
    [SerializeField] bool displayCellDistanceGizmos = false;

    Vector2Int startPos;
    Cell[,] maze;
    CellData[,] mazeData;
    MazePopulator mazePopulator;

    private void Awake()
    {
        CreateSingleton();

        if (forcedMazeSeed != 0)
        {
            Random.InitState(forcedMazeSeed);
        }
        
        startPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        maze = GenerateMaze(width, height, adjacentCellChance, startPos);
        mazeData = new CellData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mazeData[x, y] = new CellData();
            }
        }

        mazePopulator = new MazePopulator(maze, mazeData, startPos);
    }

    private void CreateSingleton()
    {
        if (Instance && Instance != this)
        {
            Debug.LogError("Found more than one MAze script instances");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        mazePopulator.PopulateMaze();
        SetTiles();
        CreateEntrance();
        CreateExit();
    }

    private void SetTiles()
    {
        Vector3Int pos = new Vector3Int(0, 0, 0);

        foreach (Cell cell in maze)
        {
            pos.x = cell.x;
            pos.y = cell.y;

            if (cell.isPath)
            {
                pathTilemap.SetTile(pos, pathTile);
            }
            else
            {
                wallTilemap.SetTile(pos, wallTile);
            }
        }

        SetBorderTiles();
    }

    private void SetBorderTiles()
    {
        Vector3Int pos = new Vector3Int(0, 0, 0);

        for (int x = 0 - borderWallWidth; x < width + borderWallWidth; x++)
        {
            pos.x = x;

            for (int y = -1; y >= -borderWallWidth; y--)
            {
                pos.y = y;
                wallTilemap.SetTile(pos, wallTile);
            }

            for (int y = height; y < height + borderWallWidth; y++)
            {
                pos.y = y;
                wallTilemap.SetTile(pos, wallTile);
            }
        }

        for (int y = 0; y < height; y++)
        {
            pos.y = y;


            for (int x = -1; x >= -borderWallWidth; x--)
            {
                pos.x = x;
                wallTilemap.SetTile(pos, wallTile);
            }

            for (int x = height; x < width + borderWallWidth; x++)
            {
                pos.x = x;
                wallTilemap.SetTile(pos, wallTile);
            }
        }
    }

    private void CreateEntrance()
    {
        Instantiate(entrancePrefab, MazeToWorldPos(startPos), Quaternion.identity);
    }

    private void CreateExit()
    {
        Instantiate(exitPrefab, MazeToWorldPos(mazePopulator.ExitPos), Quaternion.identity);
    }

    public Vector2Int GetStartPos()
    {
        return startPos;
    }

    public Vector2 MazeToWorldPos(Vector2Int pos)
    {
        return pathTilemap.GetCellCenterWorld(new Vector3Int(pos.x, pos.y, 0));
    }

    public Vector2Int WorldToMazePos(Vector2 pos)
    {
        Vector3Int mazePos = pathTilemap.WorldToCell(new Vector3(pos.x, pos.y, 0));
        return new Vector2Int(mazePos.x, mazePos.y);
    }

    public bool CanMoveTo(Vector2Int pos)
    {
        if (IsPosOutOfBounds(pos))
        { 
            return false;
        }

        if (!maze[pos.x, pos.y].isPath)
        {
            return false;
        }

        if (mazeData[pos.x, pos.y].occupant != null)
        {
            return false;
        }

        return true;
    }

    public void MoveUnitTo(MazeUnit unit, Vector2Int pos)
    {
        if (IsPosOutOfBounds(pos))
        {
            Debug.LogError(unit.name + ": Trying to move out of bounds");
        }

        CellData cellData = mazeData[pos.x, pos.y];

        if (cellData.occupant != null && cellData.occupant != unit)
        {
            Debug.LogError(unit.name + ": Trying to move unit to occupied space");
            return;
        }

        mazeData[unit.GetMovement().GetMazePos().x, unit.GetMovement().GetMazePos().y].occupant = null;
        cellData.occupant = unit;
    }

    public MazeUnit GetOccupant(Vector2Int pos)
    {
        if (IsPosOutOfBounds(pos))
        {
            return null;
        }

        return mazeData[pos.x, pos.y].occupant;
    }

    public Item GetItem(Vector2Int pos)
    {
        if (IsPosOutOfBounds(pos))
        {
            return null;
        }

        return mazeData[pos.x, pos.y].item;
    }

    public void RemoveItemAt(Vector2Int pos)
    {
        if (IsPosOutOfBounds(pos)) { return; }

        mazeData[pos.x, pos.y].item = null;
    }

    private bool IsPosOutOfBounds(Vector2Int pos)
    {
        return pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height;
    }

    public List<Vector2Int> GetValidNeighbourCoords(Vector2Int pos)
    {
        List<Vector2Int> validNeighbours = new List<Vector2Int>();

        Vector2Int possibleNeighbour = pos + Vector2Int.up;
        if (CanMoveTo(possibleNeighbour))
        {
            validNeighbours.Add(possibleNeighbour);
        }

        possibleNeighbour = pos + Vector2Int.down;
        if (CanMoveTo(possibleNeighbour))
        {
            validNeighbours.Add(possibleNeighbour);
        }

        possibleNeighbour = pos + Vector2Int.left;
        if (CanMoveTo(possibleNeighbour))
        {
            validNeighbours.Add(possibleNeighbour);
        }

        possibleNeighbour = pos + Vector2Int.right;
        if (CanMoveTo(possibleNeighbour))
        {
            validNeighbours.Add(possibleNeighbour);
        }

        return validNeighbours;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!displayCellDistanceGizmos || maze == null) { return; }

        Vector3Int pos = new Vector3Int(0, 0, 0);
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.MiddleCenter;

        Vector3 labelOffset = new Vector3(0, pathTilemap.cellSize.y / 4f);

        foreach (Cell cell in maze)
        {
            if (cell.isPath)
            {
                pos.x = cell.x;
                pos.y = cell.y;
                Vector3 labelPos = pathTilemap.GetCellCenterWorld(pos);

                Handles.Label(labelPos + labelOffset, cell.distanceFromStart.ToString(), style);
            }
        }
    }
#endif
}
