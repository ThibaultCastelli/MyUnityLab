using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class PathNode
    {
        #region Variables
        GridMap<PathNode> grid;

        List<PathNode> neighbours = new List<PathNode>();

        int x;
        int y;
        Vector2 pos;

        int gCost = int.MaxValue;       // Walking cost from the start node
        int hCost = 0;                  // Heuristic cost to reach end node
        int fCost = 0;                  // gCost + hCost

        bool isWalkable = true;

        public PathNode cameFromNode = null;
        #endregion

        #region Properties
        public int X => x;
        public int Y => y;
        public Vector2 Pos => pos;
        public int GCost
        {
            set
            {
                if (value < 0)
                    gCost = 0;
                else
                    gCost = value;
            }
            get => gCost;
        }
        public int HCost
        {
            set
            {
                if (value < 0)
                    hCost = 0;
                else
                    hCost = value;
            }
            get => hCost;
        }
        public int FCost => fCost;
        public bool IsWalkable => isWalkable;
        public List<PathNode> Neighbours => neighbours;
        #endregion

        #region Constructor
        public PathNode(GridMap<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            pos = new Vector2(x, y);
        }
        #endregion

        #region Functions
        public void SetFCost()
        {
            fCost = gCost + hCost;
        }

        public void SetWalkable()
        {
            isWalkable = !isWalkable;

            // Check if the diagonal neighbours are not walkable, if so, prevent the player to go through
            if (x - 1 >= 0)
            {
                // Left down not walkable
                if (y - 1 >= 0 && !grid.GetGridObject(x - 1, y - 1).IsWalkable)
                {
                    grid.GetGridObject(x - 1, y).SetNeighbourUnwalkable();
                    grid.GetGridObject(x, y - 1).SetNeighbourUnwalkable();
                }
                // Left up not walkable
                if (y + 1 < grid.Height && !grid.GetGridObject(x - 1, y + 1).IsWalkable)
                {
                    grid.GetGridObject(x - 1, y).SetNeighbourUnwalkable();
                    grid.GetGridObject(x, y + 1).SetNeighbourUnwalkable();
                }
            }

            if (x + 1 < grid.Width)
            {
                // Right down not walkable
                if (y - 1 >= 0 && !grid.GetGridObject(x + 1, y - 1).IsWalkable)
                {
                    grid.GetGridObject(x, y - 1).SetNeighbourUnwalkable();
                    grid.GetGridObject(x + 1, y).SetNeighbourUnwalkable();
                }
                //Righ up not walkable
                if (y + 1 < grid.Height && !grid.GetGridObject(x + 1, y + 1).isWalkable)
                {
                    grid.GetGridObject(x, y + 1).SetNeighbourUnwalkable();
                    grid.GetGridObject(x + 1, y).SetNeighbourUnwalkable();
                }
            }

            grid.OnGridValueChanged?.Invoke(x, y);
        }

        void SetNeighbourUnwalkable()
        {
            isWalkable = false;
        }

        public void FindNeighbours()
        {
            // Left
            if (x - 1 >= 0)
            {
                neighbours.Add(grid.GetGridObject(x - 1, y));

                // Left down
                if (y - 1 >= 0)
                    neighbours.Add(grid.GetGridObject(x - 1, y - 1));

                //Left up
                if (y + 1 < grid.Height)
                    neighbours.Add(grid.GetGridObject(x - 1, y + 1));
            }

            // Right
            if (x + 1 < grid.Width)
            {
                neighbours.Add(grid.GetGridObject(x + 1, y));

                // Right down
                if (y - 1 >= 0)
                    neighbours.Add(grid.GetGridObject(x + 1, y - 1));

                // Right up
                if (y + 1 < grid.Height)
                    neighbours.Add(grid.GetGridObject(x + 1, y + 1));
            }

            // Up
            if (y + 1 < grid.Height)
                neighbours.Add(grid.GetGridObject(x, y + 1));

            // Down
            if (y - 1 >= 0)
                neighbours.Add(grid.GetGridObject(x, y - 1));
        }

        public override string ToString()
        {
            return x + ", " + y;
        }
        #endregion
    }
}