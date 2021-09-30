using System;
using UnityEngine;
using TMPro;

namespace PathFindingTC
{
    public class GridMap<T>
    {
        #region Variables
        T[,] grid;
        TextMeshPro[,] debugGrid;

        Vector3 origin = Vector3.zero;

        int width;
        int height;
        float cellSize;

        bool drawGrid = true;                               // Set to true to draw the grid and display values

        Transform parent;                                   // Where the TMPro will be create for debug

        public Action<int, int> OnGridValueChanged;         // Method called when an object is changed in the grid
        Func<GridMap<T>, int, int, T> createObjectFunc;     // Method to create a default grid object
        #endregion

        #region Properties
        public Vector3 Origin => origin;
        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        #endregion

        #region Constructor
        public GridMap(Vector3 origin, int width, int height, float cellSize, Func<GridMap<T>, int, int, T> createObjectFunc, Transform parent = null)
        {
            // Set variables
            this.origin = origin;

            if (width < 1)
                this.width = 1;
            else
                this.width = width;

            if (height < 1)
                this.height = 1;
            else
                this.height = height;

            if (cellSize < 0)
                this.cellSize = 0.01f;
            else
                this.cellSize = cellSize;

            grid = new T[width, height];
            debugGrid = new TextMeshPro[width, height];

            this.createObjectFunc = createObjectFunc;

            this.parent = parent;

            // Set the grid with default grid objects
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    grid[x, y] = createObjectFunc(this, x, y);

            if (!drawGrid)
                return;

            // Draw the grid
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    debugGrid[x, y] = CreateTextMeshPro(grid[x, y]?.ToString(), GetWorldPos(x, y) + new Vector3(cellSize, cellSize) * 0.5f);
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
        public void SetGridObject(int x, int y, T obj)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return;

            grid[x, y] = obj;
            OnGridValueChanged?.Invoke(x, y);
        }
        public void SetGridObject(Vector3 worldPos, T obj)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            SetGridObject(x, y, obj);
        }

        public T GetGridObject(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
                return default(T);

            return grid[x, y];
        }
        public T GetGridObject(Vector3 worldPos)
        {
            int x, y;
            GetCoordonates(worldPos, out x, out y);
            return GetGridObject(x, y);
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

        public void ResetGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = createObjectFunc(this, x, y);
                    OnGridValueChanged?.Invoke(x, y);
                }
            }
        }
        #endregion

        #region Utilities
        void UpdateDebugGrid(int x, int y)
        {
            debugGrid[x, y].text = grid[x, y]?.ToString();
        }

        TextMeshPro CreateTextMeshPro(string txt, Vector3 pos)
        {
            GameObject textGO = new GameObject("Debug_TMPro_Grid", typeof(TextMeshPro));
            textGO.transform.SetParent(parent);
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
