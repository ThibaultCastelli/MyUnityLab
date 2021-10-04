using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class PathNode
    {
        #region Variables
        GridMap<PathNode> grid;

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
            grid.OnGridValueChanged?.Invoke(x, y);
        }

        public override string ToString()
        {
            return x + ", " + y;
        }
        #endregion
    }
}