using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static MazePopulator;

public static class PopulationUtils
{
    public static int CountAdjcentPaths(PopulationData data, Cell cell)
    {
        int count = 0;
        int x = cell.x;
        int y = cell.y;

        if (x + 1 < data.maze.GetLength(0) && data.maze[x + 1, y].isPath)
        {
            count++;
        }

        if (x - 1 >= 0 && data.maze[x - 1, y].isPath)
        {
            count++;
        }

        if (y + 1 < data.maze.GetLength(1) && data.maze[x, y + 1].isPath)
        {
            count++;
        }

        if (y - 1 >= 0 && data.maze[x, y - 1].isPath)
        {
            count++;
        }

        return count;
    }

    public static Cell GetRandomFreeUnitCell(PopulationData data)
    {
        Cell cell;
        Vector2Int pos = new Vector2Int();

        do
        {
            cell = data.mazePath[Random.Range(0, data.mazePath.Count)];
            pos.x = cell.x;
            pos.y = cell.y;
        }
        while (pos.Equals(data.startPos) || pos.Equals(data.exitPos) || !Maze.Instance.CanMoveTo(pos));

        return cell;
    }

    public static Cell GetRandomFreeItemCell(PopulationData data)
    {
        Cell cell;
        Vector2Int pos = new Vector2Int();

        do
        {
            cell = data.mazePath[Random.Range(0, data.mazePath.Count)];
            pos.x = cell.x;
            pos.y = cell.y;
        }
        while (pos.Equals(data.startPos) || pos.Equals(data.exitPos) || data.mazeData[pos.x, pos.y].item != null);

        return cell;
    }

    public static T InstantiateInCell<T>(T prefab, Cell cell) where T : Object
    {
        return Object.Instantiate(prefab, Maze.Instance.MazeToWorldPos(new Vector2Int(cell.x, cell.y)), Quaternion.identity);
    }
}
