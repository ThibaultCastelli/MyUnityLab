using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    public class GridMap
    {
        #region Variables
        int[,] grid;
        TextMesh[,] debugGrid;      // Used to draw the grid

        Vector3 origin = Vector3.zero;

        int width;
        int height;
        float cellSize;
        #endregion

        #region Constructors
        public GridMap(int width, int height, float cellSize, Vector3 origin)
        {
            // Set values
            if (width < 1)
                width = 1;
            else
                this.width = width;

            if (height < 1)
                height = 1;
            else
                this.height = height;

            if (cellSize < 0)
                cellSize = 0.01f;
            else
                this.cellSize = cellSize;

            this.origin = origin;

            grid = new int[width, height];
            debugGrid = new TextMesh[width, height];

            // Draw the grid
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    debugGrid[i, j] = CreateTextMesh(grid[i, j].ToString(), GetWorldPos(i, j) + new Vector3(cellSize, cellSize) * 0.5f);
                    Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i + 1, j), Color.white, 100f);
                    Debug.DrawLine(GetWorldPos(i, j), GetWorldPos(i, j + 1), Color.white, 100f);
                }
                Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(width, height), GetWorldPos(width, 0), Color.white, 100f);
            }
        }
        #endregion

        #region Functions
        public void SetValue(int x, int y, int value)
        {
            // Prevent errors
            if (x < 0 || y < 0)
            {
                Debug.LogError("ERROR : Index out of bounds, x or y can't be negative.");
                return;
            }
            if (x >= width || y >= height)
            {
                Debug.LogError("ERROR : Index out of bounds, x or y can't be greater than width or height.");
                return;
            }

            grid[x, y] = value;
            debugGrid[x, y].text = value.ToString();
        }
        public void SetValue(Vector3 worldPos, int value)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            SetValue(x, y, value);
        }

        public void GetValue(int x, int y)
        {
            Debug.Log(grid[x, y]);
        }
        public void GetValue(Vector3 worldPos)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            GetValue(x, y);
        }

        public void GetCoordonates(Vector3 worldPos, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPos - origin).x / cellSize);
            y = Mathf.FloorToInt((worldPos - origin).y / cellSize);
        }

        public Vector3 GetWorldPos(int x, int y)
        {
            return (new Vector3(x, y) * cellSize) + origin;
        }
        #endregion

        #region Utility
        TextMesh CreateTextMesh(string txt, Vector3 pos)
        {
            GameObject textGO = new GameObject("TextMesh", typeof(TextMesh));
            textGO.transform.localPosition = pos;

            TextMesh text = textGO.GetComponent<TextMesh>();

            text.text = txt;
            text.fontSize = 40;
            text.anchor = TextAnchor.MiddleCenter;

            return text;
        }
        #endregion
    }
}
