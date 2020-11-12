using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MazeGeneration
{
    public class Cell
    {
        public int x;
        public int y;
        public bool isPath;
        public int neighbourCount;
        public int distanceFromStart;

        public Cell(int x, int y, bool isPath)
        {
            this.x = x;
            this.y = y;
            this.isPath = isPath;
            this.neighbourCount = 0;
            this.distanceFromStart = int.MaxValue;
        }
    }

    public static Cell[,] GenerateMaze(int width, int height, float adjCellChance)
    {
        return GenerateMaze(width, height, adjCellChance, new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0));
    }

    public static Cell[,] GenerateMaze(int width, int height, float adjCellChance, Vector3Int startingCellPos)
    {
        Cell[,] maze = new Cell[width, height];
        initalizeMaze(maze);

        List<Cell> potentialPaths = new List<Cell>();

        Cell startingCell = maze[startingCellPos.x, startingCellPos.y];
        potentialPaths.Add(startingCell);

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

        UpdateDistancesFromStart(maze, startingCell);

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
            UpdateNeighbourCell(potentialPaths, cell, maze[cell.x, cell.y + 1]);
        }

        if (cell.y > 0)
        {
            UpdateNeighbourCell(potentialPaths, cell, maze[cell.x, cell.y - 1]);
        }

        if (cell.x + 1 < maze.GetLength(0))
        {
            UpdateNeighbourCell(potentialPaths, cell, maze[cell.x + 1, cell.y]);
        }

        if (cell.x > 0)
        {
            UpdateNeighbourCell(potentialPaths, cell, maze[cell.x - 1, cell.y]);
        }
    }

    private static void UpdateNeighbourCell(List<Cell> potentialPaths, Cell updatingCell, Cell neighbourCell)
    {
        neighbourCell.neighbourCount++;
        potentialPaths.Add(neighbourCell);
    }

    private static void UpdateDistancesFromStart(Cell[,] maze, Cell startingCell)
    {
        startingCell.distanceFromStart = 0;

        List<Cell> cellsToUpdate = new List<Cell>();
        cellsToUpdate.Add(startingCell);

        while (cellsToUpdate.Count > 0)
        {
            Cell cell = cellsToUpdate[0];
            cellsToUpdate.RemoveAt(0);

            UpdatCellDistance(maze, cellsToUpdate, cell);
        }
    }

    private static void UpdatCellDistance(Cell[,] maze, List<Cell> cellsToUpdate, Cell cell)
    {
        List<Cell> validNeighbours = new List<Cell>();

        if (cell.y + 1 < maze.GetLength(1))
        {
            validNeighbours.Add(maze[cell.x, cell.y + 1]);
        }

        if (cell.y > 0)
        {
            validNeighbours.Add(maze[cell.x, cell.y - 1]);
        }

        if (cell.x + 1 < maze.GetLength(0))
        {
            validNeighbours.Add(maze[cell.x + 1, cell.y]);
        }

        if (cell.x > 0)
        {
            validNeighbours.Add(maze[cell.x - 1, cell.y]);
        }

        Cell closestCell = cell;
        foreach (Cell neighbour in validNeighbours)
        {
            if (neighbour.isPath && 
                neighbour.distanceFromStart < closestCell.distanceFromStart)
            {
                closestCell = neighbour;
            }
        }

        if (closestCell != cell)
        {
            cell.distanceFromStart = closestCell.distanceFromStart + 1;
        }

        foreach (Cell neighbour in validNeighbours)
        {
            if (neighbour.isPath &&
                cell.distanceFromStart < neighbour.distanceFromStart)
            {
                cellsToUpdate.Add(neighbour);
            }
        }
    }
}
