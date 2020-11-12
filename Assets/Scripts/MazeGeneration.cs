﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeGeneration
{
    public class Cell
    {
        public int x;
        public int y;
        public bool isPath;
        public int neighbourCount;

        public Cell(int x, int y, bool isPath)
        {
            this.x = x;
            this.y = y;
            this.isPath = isPath;
            this.neighbourCount = 0;
        }
    }

    public static Cell[,] GenerateMaze(int width, int height, float adjCellChance)
    {
        return GenerateMaze(width, height, adjCellChance, new Vector2Int(Random.Range(0, width), Random.Range(0, height)));
    }

    public static Cell[,] GenerateMaze(int width, int height, float adjCellChance, Vector2Int startingCell)
    {
        Cell[,] maze = new Cell[width, height];
        initalizeMaze(maze);

        List<Cell> potentialPaths = new List<Cell>();
        
        potentialPaths.Add(maze[startingCell.x, startingCell.y]);

        while (potentialPaths.Count > 0)
        {
            int randomPathPos = Random.Range(0, potentialPaths.Count);
            Cell selectedCell = potentialPaths[randomPathPos];

            potentialPaths.RemoveAt(randomPathPos);

            if (selectedCell.neighbourCount <= 1)
            {
                SetCellAsPathAndUpdateValidNeighbours(maze, potentialPaths, selectedCell);
            }
            else if (Random.Range(0f, 1f) < adjCellChance)
            {
                SetCellAsPathAndUpdateValidNeighbours(maze, potentialPaths, selectedCell);
            }
        }

        return maze;
    }

    private static void initalizeMaze(Cell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new Cell(x, y, false);
            }
        }
    }

    private static void SetCellAsPathAndUpdateValidNeighbours(Cell[,] maze, List<Cell> potentialPaths, Cell cell)
    {
        cell.isPath = true;

        if (cell.y + 1 < maze.GetLength(1))
        {
            UpdateNeighbourCell(potentialPaths, maze[cell.x, cell.y + 1]);
        }

        if (cell.y > 0)
        {
            UpdateNeighbourCell(potentialPaths, maze[cell.x, cell.y - 1]);
        }

        if (cell.x + 1 < maze.GetLength(0))
        {
            UpdateNeighbourCell(potentialPaths, maze[cell.x + 1, cell.y]);
        }

        if (cell.x > 0)
        {
            UpdateNeighbourCell(potentialPaths, maze[cell.x - 1, cell.y]);
        }
    }

    private static void UpdateNeighbourCell(List<Cell> potentialPaths, Cell cell)
    {
        cell.neighbourCount++;
        potentialPaths.Add(cell);
    }
}
