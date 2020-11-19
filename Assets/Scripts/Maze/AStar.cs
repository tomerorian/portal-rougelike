using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AStar
{
    const int MOVE_COST = 1;

    public static List<Vector2Int> FindPath(Maze maze, Vector2Int start, Vector2Int target, List<Vector2Int> goals)
    {
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        Dictionary<Vector2Int, int> gScores = new Dictionary<Vector2Int, int>();

        openSet.Enqueue(new Node(start, 0, Hueristic(start, target), null));
        gScores.Add(start, 0);

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();
            
            foreach (var goal in goals)
            {
                if (current.pos.Equals(goal))
                {
                    return RecostructPath(current);
                }
            }

            foreach (Vector2Int neighbour in maze.GetValidNeighbourCoords(current.pos))
            {
                int tentativeG = current.g + MOVE_COST;

                if (!gScores.ContainsKey(neighbour) || tentativeG < gScores[neighbour])
                {
                    Node neighbourNode = openSet.FindDequeue(node => node.pos.Equals(neighbour));

                    if (neighbourNode == null)
                    {
                        neighbourNode = new Node(neighbour);
                    }

                    neighbourNode.parent = current;
                    neighbourNode.g = tentativeG;
                    neighbourNode.f = tentativeG + Hueristic(neighbour, target);

                    if (gScores.ContainsKey(neighbour))
                    {
                        gScores[neighbour] = tentativeG;
                    }
                    else
                    {
                        gScores.Add(neighbour, tentativeG);
                    }

                    openSet.Enqueue(neighbourNode);
                }
            }
        }

        return new List<Vector2Int>();
    }

    private static List<Vector2Int> RecostructPath(Node goal)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        Node current = goal;

        while (current != null)
        {
            path.Insert(0, current.pos);
            current = current.parent;
        }

        return path;
    }

    private static int Hueristic(Vector2Int pos, Vector2Int goal)
    {
        return Math.Abs(goal.x - pos.x) + Math.Abs(goal.y - pos.y);
    }

    private class Node : IComparable<Node>
    {
        public Vector2Int pos;
        public int g;
        public int f;
        public Node parent;

        public Node(Vector2Int pos)
        {
            this.pos = pos;
            g = 0;
            f = 0;
            parent = null;
        }

        public Node(Vector2Int pos, int g, int f, Node parent)
        {
            this.pos = pos;
            this.g = g;
            this.f = f;
            this.parent = parent;
        }

        public int CompareTo(Node other)
        {
            return this.f - other.f;
        }

        public override bool Equals(object obj)
        {
            Node other = (Node)obj;
            return pos.Equals(other.pos);
        }

        public override int GetHashCode()
        {
            return 991532785 + pos.GetHashCode();
        }
    }
}
