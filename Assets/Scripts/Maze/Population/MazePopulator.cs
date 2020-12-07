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
    const int MAX_FAILED_SPAWN_ATTEMPTS = 10;

    PopulationData data;

    public Vector2Int ExitPos { get { return data.exitPos; } }

    SpawnRule[] enemySpawnRules;
    SpawnRule[] itemSpawnRules;

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
        enemySpawnRules = new SpawnRule[] {
            new SpawnSlime(1, 1),
            new SpawnSkeleton(4, 3),
        };

        itemSpawnRules = new SpawnRule[] {
            new SpawnHealthPotion(1, 2),
            new SpawnSword(1, 3),
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
        PopulateRules(enemySpawnRules);
    }
    #endregion

    #region Item Population
    private void PopulateItems()
    {
        PopulateRules(itemSpawnRules);
    }
    #endregion

    private void PopulateRules(SpawnRule[] rules)
    {
        int level = GameSession.Instance.GetLevel();
        int relativeDifficulty = level; // What rules can spawn
        int difficulty = Mathf.FloorToInt(5f * (1 + Mathf.Log(level, 2))); // How many of them can spawn

        int failedAttempts = 0;

        while (difficulty > 0 && failedAttempts < MAX_FAILED_SPAWN_ATTEMPTS)
        {
            failedAttempts++;

            SpawnRule rule = rules[Random.Range(0, rules.Length)];

            if (rule.GetRelativeDifficulty() <= relativeDifficulty && rule.GetTotalDifficulty() <= difficulty)
            {
                if (rule.AttemptSpawn(data))
                {
                    difficulty -= rule.GetTotalDifficulty();
                    failedAttempts = 0;
                }
            }
        }
    }

    #endregion
}
