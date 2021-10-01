using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class PathFinding
    {
        #region Variables
        GridMap<PathNode> grid;

        List<PathNode> openList;        // The nodes that need to be search
        List<PathNode> closedList;      // List of nodes that had already been searched

        const int STARIGHT_MOVE_COST = 10;
        const int DIAGONAL_MOVE_COST = 14;

        static PathFinding instance;    // Singleton
        #endregion

        #region Properties
        public GridMap<PathNode> Grid => grid;
        public static PathFinding Instance => instance;
        #endregion

        #region Constructor
        public PathFinding(Vector3 origin, int width, int height, int cellSize)
        {
            instance = this;

            grid = new GridMap<PathNode>(origin, width, height, cellSize, (GridMap<PathNode> g, int x, int y) => new PathNode(g, x, y));
            openList = new List<PathNode>();
            closedList = new List<PathNode>();
        }
        #endregion

        #region Functions
        public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
        {
            List<Vector3> pathPos = new List<Vector3>();
            int xStart, yStart, xEnd, yEnd;
            grid.GetCoordonates(startPos, out xStart, out yStart);
            grid.GetCoordonates(endPos, out xEnd, out yEnd);

            List<PathNode> pathNode = FindPath(xStart, yStart, xEnd, yEnd);
            if (pathNode == null)
                return null;

            foreach (PathNode node in pathNode)
                pathPos.Add(new Vector3(node.X, node.Y) * grid.CellSize + grid.Origin + new Vector3(1, 1) * grid.CellSize * 0.5f);

            return pathPos;
        }
        public List<PathNode> FindPath(int xStart, int yStart, int xEnd, int yEnd)
        {
            // Clear the lists from previous operations
            openList.Clear();
            closedList.Clear();

            // Clear all the path nodes
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    PathNode node = grid.GetGridObject(x, y);
                    node.GCost = int.MaxValue;
                    node.cameFromNode = null;
                }
            }

            // Set the end node
            PathNode endNode = grid.GetGridObject(xEnd, yEnd);

            // Set start node
            PathNode startNode = grid.GetGridObject(xStart, yStart);
            startNode.HCost = CalculateDistance(startNode, endNode);
            startNode.SetFCost();
            openList.Add(startNode);

            // Search a path while there is nodes to search in the open list
            while (openList.Count > 0)
            {
                // Take the node with the lowestFCost 
                PathNode currentNode = FindLowestFCost(openList);

                // Check if this node is the end node, if so, return the path
                if (currentNode == endNode)
                {
                    List<PathNode> path = new List<PathNode>();
                    PathNode current = endNode;

                    // Start from the end node and get all the came from nodes
                    path.Add(current);
                    while (current.cameFromNode != null)
                    {
                        path.Add(current.cameFromNode);
                        current = current.cameFromNode;
                    }

                    // Reverse the path to get the path from start to end
                    path.Reverse();
                    return path;
                }

                // Take the node with the lowest fCost to be the next node on the path
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                // Search for the neighbours of this node and add them to the open list
                foreach (PathNode neighbour in FindNeighbours(currentNode))
                {
                    // If the node has already been search or has already been set, skip
                    if (closedList.Contains(neighbour))
                        continue;

                    // If the node is not walkable just add to the closed list to be ignore next times
                    if (!neighbour.IsWalkable)
                    {
                        closedList.Add(neighbour);
                        continue;
                    }

                    // Check if the gCost from the current node to the neighbour is less than another previous node
                    int gCostTentative = currentNode.GCost + CalculateDistance(currentNode, neighbour);

                    // Set the neighbour node if the gCost is less than the previous
                    if (gCostTentative < neighbour.GCost)
                    {
                        neighbour.GCost = currentNode.GCost + CalculateDistance(currentNode, neighbour);
                        neighbour.HCost = CalculateDistance(neighbour, endNode);
                        neighbour.SetFCost();
                        neighbour.cameFromNode = currentNode;

                        if (!openList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                        }
                    }
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
        #endregion
    }
}
