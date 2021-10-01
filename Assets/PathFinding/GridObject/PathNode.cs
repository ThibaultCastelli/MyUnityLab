using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class PathNode
    {
        GridMap<PathNode> grid;

        int x;
        int y;

        int gCost = 0;      // Walking cost from the start node
        int hCost = 0;      // Heuristic cost to reach end node
        int fCost = 0;      // gCost + hCost

        public PathNode previousNode = null;

        public int X => x;
        public int Y => y;
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

        public PathNode(GridMap<PathNode> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }

        public override string ToString()
        {
            return x + ", " + y;
        }
    }
}