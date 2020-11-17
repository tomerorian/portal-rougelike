﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;

public class MazePopulation
{
    const int EXIT_DISTANCE_GAP_FROM_MAX = 5;

    Cell[,] maze;
    List<Cell> mazePath;
    Vector2Int startPos;
    int maxDistanceFromStart;

    public Vector2Int exitPos { get; private set; }

    public MazePopulation(Cell[,] maze, Vector2Int startPos)
    {
        this.maze = maze;
        this.startPos = startPos;

        mazePath = new List<Cell>();
    }

    public void PopulateMaze()
    {
        InitMazePath();
        FindMaxDistanceFromStart();
        PlaceExit();
    }

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

    private void PlaceExit()
    {
        int minDistanceForExit = maxDistanceFromStart - EXIT_DISTANCE_GAP_FROM_MAX;

        List<Cell> exitOptions = new List<Cell>();

        foreach (Cell cell in mazePath)
        {
            if (cell.distanceFromStart >= minDistanceForExit &&
                CountAdjcentPaths(cell) == 1)
            {
                exitOptions.Add(cell);
            }
        }

        Cell exitCell = exitOptions[Random.Range(0, exitOptions.Count)];
        exitPos = new Vector2Int(exitCell.x, exitCell.y);
    }

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
}
