using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;

public class MazePopulator
{
    const int EXIT_DISTANCE_GAP_FROM_MAX = 5;

    Cell[,] maze;
    CellData[,] mazeData;
    List<Cell> mazePath;
    Vector2Int startPos;
    int maxDistanceFromStart;

    public Vector2Int ExitPos { get; private set; }

    public MazePopulator(Cell[,] maze, CellData[,] mazeData, Vector2Int startPos)
    {
        this.maze = maze;
        this.mazeData = mazeData;
        this.startPos = startPos;

        mazePath = new List<Cell>();
    }

    public void PopulateMaze()
    {
        InitMazePath();
        FindMaxDistanceFromStart();

        PlaceExit();
        PopulateEnemies();
        PopulateItems();
    }

    #region Prep
    private void InitMazePath()
    {
        foreach (Cell cell in maze)
        {
            if (cell.isPath)
            {
                mazePath.Add(cell);
            }
        }
    }

    private void FindMaxDistanceFromStart()
    {
        maxDistanceFromStart = 0;

        foreach (Cell cell in mazePath)
        {
            if (cell.distanceFromStart > maxDistanceFromStart)
            {
                maxDistanceFromStart = cell.distanceFromStart;
            }
        }
    }
    #endregion

    #region Population
    private void PlaceExit(int maxAdjcentCells = 1)
    {
        if (maxAdjcentCells > 4)
        {
            Debug.LogError("Couldn't place exit after 4 tries");
            return;
        }

        int minDistanceForExit = maxDistanceFromStart - EXIT_DISTANCE_GAP_FROM_MAX;

        List<Cell> exitOptions = new List<Cell>();

        foreach (Cell cell in mazePath)
        {
            if (cell.distanceFromStart >= minDistanceForExit &&
                CountAdjcentPaths(cell) <= maxAdjcentCells)
            {
                exitOptions.Add(cell);
            }
        }

        if (exitOptions.Count == 0)
        {
            PlaceExit(maxAdjcentCells + 1);
        }
        else
        {
            Cell exitCell = exitOptions[Random.Range(0, exitOptions.Count)];
            ExitPos = new Vector2Int(exitCell.x, exitCell.y);
        }
    }

    #region Enemy Population
    private void PopulateEnemies()
    {
        int level = GameSession.Instance.GetLevel();
        int enemiesToPopulate = Mathf.FloorToInt(5f * (1 + Mathf.Log(level, 2)));

        for (int i = 0; i < enemiesToPopulate; i++)
        {
            Cell randomSpawnPoint = GetRandomFreePath();

            InstantiateInCell(PrefabCache.Instance.Enemy, randomSpawnPoint);
        }
    }
    #endregion

    #region Item Population
    private void PopulateItems()
    {
        Cell randomFreePoint = GetRandomFreePath();

        InstantiateInCell(PrefabCache.Instance.Sword, randomFreePoint);
    }
    #endregion

    #endregion

    #region Utils
    private int CountAdjcentPaths(Cell cell)
    {
        int count = 0;
        int x = cell.x;
        int y = cell.y;

        if (x + 1 < maze.GetLength(0) && maze[x + 1, y].isPath)
        {
            count++;
        }

        if (x - 1 >= 0 && maze[x - 1, y].isPath)
        {
            count++;
        }

        if (y + 1 < maze.GetLength(1) && maze[x, y + 1].isPath)
        {
            count++;
        }

        if (y - 1 >= 0 && maze[x, y - 1].isPath)
        {
            count++;
        }

        return count;
    }

    private Cell GetRandomFreePath()
    {
        Cell cell;
        Vector2Int pos = new Vector2Int();

        do
        {
            cell = mazePath[Random.Range(0, mazePath.Count)];
            pos.x = cell.x;
            pos.y = cell.y;
        }
        while (pos.Equals(startPos) || pos.Equals(ExitPos) || !Maze.Instance.CanMoveTo(pos));

        return cell;
    }

    private T InstantiateInCell<T>(T prefab, Cell cell) where T : Object
    {
        return Object.Instantiate(prefab, Maze.Instance.MazeToWorldPos(new Vector2Int(cell.x, cell.y)), Quaternion.identity);
    }
    #endregion
}
