using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class PathFinding
    {
        GridMap<PathNode> grid;

        List<PathNode> openList;        // The nodes that need to be search
        List<PathNode> closedList;      // The nodes already search

        const int STARIGHT_MOVE_COST = 10;
        const int DIAGONAL_MOVE_COST = 14;

        public GridMap<PathNode> Grid => grid;

        public PathFinding(Vector3 origin, int width, int height, int cellSize)
        {
            grid = new GridMap<PathNode>(origin, width, height, cellSize, (GridMap<PathNode> g, int x, int y) => new PathNode(g, x, y));
            openList = new List<PathNode>();
            closedList = new List<PathNode>();
        }

        public List<PathNode> FindPath(int xStart, int yStart, int xEnd, int yEnd)
        {
            // Clear the lists from previous operations
            openList.Clear();
            closedList.Clear();

            // Set the end node
            PathNode endNode = grid.GetGridObject(xEnd, yEnd);

            // Set start node
            PathNode startNode = grid.GetGridObject(xStart, yStart);
            startNode.HCost = CalculateDistance(startNode, endNode);
            startNode.CalculateFCost();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // Take the node with the lowestFCost 
                PathNode currentNode = FindLowestFCost(openList);

                // Check if this node is the end node, if so, return the closed list reverse
                if (currentNode == endNode)
                {
                    List<PathNode> path = new List<PathNode>();
                    path.Add(endNode);
                    PathNode current = endNode;
                    while (current.previousNode != null)
                    {
                        path.Add(current.previousNode);
                        current = current.previousNode;
                    }
                    path.Reverse();
                    return path;
                }
                Debug.Log(currentNode);
                // Add the lowest fCost node from the open list to the closed list
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                // Search for the neighbours of this node and add them to the open list
                foreach (PathNode neighbour in FindNeighbours(currentNode))
                {
                    if (closedList.Contains(neighbour) || openList.Contains(neighbour))
                        continue;

                    neighbour.GCost = currentNode.GCost + CalculateDistance(currentNode, neighbour);
                    neighbour.HCost = CalculateDistance(neighbour, endNode);
                    neighbour.CalculateFCost();
                    neighbour.previousNode = currentNode;

                    openList.Add(neighbour);
                }

            }
            // If goes out of the loop (open list is empty) return null (no path find)
            return null;
        }

        int CalculateDistance(PathNode startNode, PathNode endNode)
        {
            int xDistance = Mathf.Abs(startNode.X - endNode.X);
            int yDistance = Mathf.Abs(startNode.Y - endNode.Y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return DIAGONAL_MOVE_COST * Mathf.Min(xDistance, yDistance) + STARIGHT_MOVE_COST * remaining;
        }

        PathNode FindLowestFCost(List<PathNode> nodes)
        {
            PathNode lowestFCost = nodes[0];
            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i].FCost < lowestFCost.FCost)
                    lowestFCost = nodes[i];
            }
            return lowestFCost;
        }

        List<PathNode> FindNeighbours(PathNode node)
        {
            List<PathNode> neighbours = new List<PathNode>();

            // Left
            if (node.X - 1 >= 0)
            {
                neighbours.Add(grid.GetGridObject(node.X - 1, node.Y));

                // Left down
                if (node.Y - 1 >= 0)
                    neighbours.Add(grid.GetGridObject(node.X - 1, node.Y - 1));

                //Left up
                if (node.Y + 1 < grid.Height)
                    neighbours.Add(grid.GetGridObject(node.X - 1, node.Y + 1));
            }

            // Right
            if (node.X + 1 < grid.Width)
            {
                neighbours.Add(grid.GetGridObject(node.X + 1, node.Y));

                // Right down
                if (node.Y - 1 >= 0)
                    neighbours.Add(grid.GetGridObject(node.X + 1, node.Y - 1));

                // Right up
                if (node.Y + 1 < grid.Height)
                    neighbours.Add(grid.GetGridObject(node.X + 1, node.Y + 1));
            }

            // Up
            if (node.Y + 1 < grid.Height)
                neighbours.Add(grid.GetGridObject(node.X, node.Y + 1));

            // Down
            if (node.Y - 1 >= 0)
                neighbours.Add(grid.GetGridObject(node.X, node.Y - 1));

            return neighbours;
        }
    }
}
