using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public class MazePopulator
{
    public struct PopulationData
    {
        public Cell[,] maze;
        public CellData[,] mazeData;
        public List<Cell> mazePath;
        public Vector2Int startPos;
        public Vector2Int exitPos;
        public int maxDistanceFromStart;
    }

    const int EXIT_DISTANCE_GAP_FROM_MAX = 5;

    PopulationData data;

    public Vector2Int ExitPos { get { return data.exitPos; } }

    SpawnRule[] spawnRules;

    public MazePopulator(Cell[,] maze, CellData[,] mazeData, Vector2Int startPos)
    {
        data.maze = maze;
        data.mazeData = mazeData;
        data.startPos = startPos;

        data.mazePath = new List<Cell>();

        InitSpawnRules();
    }

    private void InitSpawnRules()
    {
        spawnRules = new SpawnRule[] {
            new SpawnSlime(1, 1),
            new SpawnSkeleton(4, 3),
        };
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
        foreach (Cell cell in data.maze)
        {
            if (cell.isPath)
            {
                data.mazePath.Add(cell);
            }
        }
    }

    private void FindMaxDistanceFromStart()
    {
        data.maxDistanceFromStart = 0;

        foreach (Cell cell in data.mazePath)
        {
            if (cell.distanceFromStart > data.maxDistanceFromStart)
            {
                data.maxDistanceFromStart = cell.distanceFromStart;
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

        int minDistanceForExit = data.maxDistanceFromStart - EXIT_DISTANCE_GAP_FROM_MAX;

        List<Cell> exitOptions = new List<Cell>();

        foreach (Cell cell in data.mazePath)
        {
            if (cell.distanceFromStart >= minDistanceForExit &&
                CountAdjcentPaths(data, cell) <= maxAdjcentCells)
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
            data.exitPos = new Vector2Int(exitCell.x, exitCell.y);
        }
    }

    #region Enemy Population
    private void PopulateEnemies()
    {
        int level = GameSession.Instance.GetLevel();
        int relativeDifficulty = level; // What rules can spawn
        int difficulty = Mathf.FloorToInt(5f * (1 + Mathf.Log(level, 2))); // How many of them can spawn

        while (difficulty > 0)
        {
            SpawnRule rule = spawnRules[Random.Range(0, spawnRules.Length)];
            
            if (rule.GetRelativeDifficulty() <= relativeDifficulty && difficulty >= rule.GetTotalDifficulty())
            {
                if (rule.AttemptSpawn(data))
                {
                    difficulty -= rule.GetTotalDifficulty();
                }
            }
        }
    }
    #endregion

    #region Item Population
    private void PopulateItems()
    {
        Cell cell = data.maze[data.startPos.x, data.startPos.y]; //GetRandomFreeItemCell();

        Item item = InstantiateInCell(PrefabCache.Instance.HealthPotion, cell);
        data.mazeData[cell.x, cell.y].item = item;
    }
    #endregion

    #endregion
}
