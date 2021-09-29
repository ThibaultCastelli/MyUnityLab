using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PathFindingTC
{
    public class GridMap
    {
        #region Variables
        int[,] grid;
        TextMeshPro[,] debugGrid;      // Used to draw the grid

        Vector3 origin = Vector3.zero;

        int width;
        int height;
        float cellSize;

        int minCellValue;
        int maxCellValue;

        bool drawGrid;

        public Action<int[,], int, int> OnGridValueChanged;     // Raised when a value is changed in the grid (send as arguments the grid, the x and y coordonates modified)
        #endregion

        #region Properties
        public Vector3 Origin => origin;
        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        #endregion

        #region Constructors
        public GridMap(int width, int height, float cellSize, Vector3 origin, int minCellValue, int maxCellValue)
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
            this.minCellValue = minCellValue;
            this.maxCellValue = maxCellValue;

            grid = new int[width, height];
            debugGrid = new TextMeshPro[width, height];

            drawGrid = true;      // Set to true to draw the grid
            if (!drawGrid)
                return;

            // Draw the grid
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = minCellValue;
                    debugGrid[x, y] = CreateTextMeshPro(grid[x, y].ToString(), GetWorldPos(x, y) + new Vector3(cellSize, cellSize) * 0.5f);
                    
                    Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.white, 100f);
                    Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.white, 100f);
                }
                Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(width, height), GetWorldPos(width, 0), Color.white, 100f);
            }

            OnGridValueChanged += UpdateDebugGrid;
        }
        #endregion

        #region Functions
        public void SetValue(int x, int y, int value)
        {
            // Do nothing if out of the grid
            if (x < 0 || y < 0 || x >= width || y >= height)
                return;

            grid[x, y] = Mathf.Clamp(value, minCellValue, maxCellValue);
            OnGridValueChanged(grid, x, y);
        }
        public void SetValue(Vector3 worldPos, int value)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            SetValue(x, y, value);
        }

        public void AddValue(int x, int y, int value)
        {
            grid[x, y] = Mathf.Clamp(GetValue(x, y) + value, minCellValue, maxCellValue);
            OnGridValueChanged(grid, x, y);
        }
        public void AddValue(Vector3 worldPos, int value)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            AddValue(x, y, value);
        }

        public int GetValue(int x, int y)
        {
            return grid[x, y];
        }
        public int GetValue(Vector3 worldPos)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            return GetValue(x, y);
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
        void UpdateDebugGrid(int[,] grid, int x, int y)
        {
            if (drawGrid)
                debugGrid[x, y].text = grid[x, y].ToString();
        }

        TextMeshPro CreateTextMeshPro(string txt, Vector3 pos)
        {
            GameObject textGO = new GameObject("TextMeshPro", typeof(TextMeshPro));
            textGO.transform.localPosition = pos;

            TextMeshPro text = textGO.GetComponent<TextMeshPro>();

            text.text = txt;
            text.fontSize = 4 * (int)cellSize;
            text.alignment = TextAlignmentOptions.Center;

            return text;
        }
        #endregion
    }
}
