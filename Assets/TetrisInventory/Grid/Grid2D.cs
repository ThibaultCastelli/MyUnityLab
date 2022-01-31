using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TetrisInventoryTC
{
    public class Grid2D<T>
    {
        #region Variables
        private T[,] grid;

        private int width;
        private int height;

        private Vector2 origin;
        private float cellWidth;
        private float cellHeight;

        private static int defaultWidth = 2;
        private static int defaultHeight = 2;
        private static Vector2 defaultOrigin = Vector2.zero;
        private static float defaultCellWidth = 1;
        private static float defaultCellHeight = 1;

        private bool showGrid;
        private GameObject canvasGO;
        private GameObject[,] debugTextGrid;

        public Action OnValueChange;
        #endregion

        #region Properties
        public int Width => width;
        public int Height => height;

        public Vector2 Origin => origin;
        public float CellWidth => cellWidth;
        public float CellHeight => cellHeight;
        #endregion

        #region Constructors
        public Grid2D(bool showGrid = false) : this(defaultWidth, defaultHeight, defaultOrigin, defaultCellWidth, 
            defaultCellHeight, showGrid) { }
        public Grid2D(int width, int height, bool showGrid = false) : this(width, height, defaultOrigin, 
            defaultCellWidth, defaultCellHeight, showGrid) { }
        public Grid2D(int width, int height, float cellWidth, float cellHeight, bool showGrid = false) : this(width, height, 
            defaultOrigin, cellWidth, cellHeight, showGrid) { }

        public Grid2D(int width, int height, Vector2 origin, float cellWidth, float cellHeight, bool showGrid = false)
        {
            if (width < 1)
            {
                this.width = defaultWidth;
            }
            else
            {
                this.width = width;
            }

            if (height < 1)
            {
                this.height = defaultHeight;
            }
            else
            {
                this.height = height;
            }

            if (cellWidth <= 0)
            {
                this.cellWidth = defaultCellWidth;
            }
            else
            {
                this.cellWidth = cellWidth;
            }

            if (cellHeight <= 0)
            {
                this.cellHeight = defaultCellHeight;
            }
            else
            {
                this.cellHeight = cellHeight;
            }

            grid = new T[this.width, this.height];

            this.origin = new Vector2(origin.x - ((this.width * this.cellWidth) / 2),
                origin.y - ((this.height * this.cellHeight) / 2));

            this.showGrid = showGrid;
            if (showGrid)
            {
                OnValueChange += PrintGrid;
                InitializeGridPrint();
                PrintGrid();
            }
        }
        #endregion

        #region Functions
        public void SetCellValue(int x, int y, T newValue)
        {
            if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1))
            {
                Debug.LogError("ERROR : The coordonates of the grid are out of bounds");
                return;
            }

            grid[x, y] = newValue;
            OnValueChange?.Invoke();
        }

        public void SetCellValue(Vector2 pos, T newValue)
        {
            int x, y;
            GetCellCoordonates(pos, out x, out y);

            SetCellValue(x, y, newValue);
        }

        public T GetCellValue(int x, int y)
        {
            if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid.GetLength(1))
            {
                Debug.LogError("ERROR : The coordonates of the grid are out of bounds");
                return default(T);
            }

            return grid[x, y];
        }

        public T GetCellValue(Vector2 pos)
        {
            int x, y;
            GetCellCoordonates(pos, out x, out y);

            return GetCellValue(x, y);
        }

        public void FillGrid(T filler)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = filler;
                }
            }

            OnValueChange?.Invoke();
        }
        #endregion

        #region Helper Functions
        private Vector2 GetCelldPos(int x, int y)
        {
            float xPos = ((x * cellWidth) + origin.x) + (cellWidth / 2);
            float yPos = ((y * cellHeight) + origin.y) + (cellHeight / 2);
            return new Vector2(xPos, yPos);
        }

        private void GetCellCoordonates(Vector2 pos, out int x, out int y)
        {
            Vector3 worldPos = GetWorldPos(pos);

            x = Mathf.FloorToInt((worldPos.x - origin.x) / cellWidth);
            y = Mathf.FloorToInt((worldPos.y - origin.y) / cellHeight);
        }

        private Vector3 GetWorldPos(Vector2 screenPos)
        {
            Vector3 pos = new Vector3(screenPos.x, screenPos.y, -Camera.main.transform.position.z);
            return Camera.main.ScreenToWorldPoint(pos);
        }
        #endregion

        #region Debug Functions
        /// <summary>
        /// Only for debug, dynamically change the grid with the given values.
        /// </summary>
        public void FollowChangedValues(int newWidth, int newHeight, Vector2 newOrigin, float newCellWidth, float newCellHeight)
        {
            PrintGrid();
            ChangeGridSize(newWidth, newHeight);
            ChangeOrigin(newOrigin);
            ChangeCellSize(newCellWidth, newCellHeight);
        }

        private void ChangeGridSize(int newWidth, int newHeight)
        {
            if (newWidth <= 0 || newHeight <= 0)
            {
                return;
            }
            if (newWidth == width && newHeight == height)
            {
                return;
            }

            width = newWidth;
            height = newHeight;

            grid = new T[width, height];

            canvasGO.SetActive(false);

            if (showGrid)
            {
                InitializeGridPrint();
                PrintGrid();
            }
        }

        private void ChangeOrigin(Vector2 newOrigin)
        {
            newOrigin = new Vector2(newOrigin.x - ((width * cellWidth) / 2),
                newOrigin.y - ((height * cellHeight) / 2));

            if (newOrigin == origin)
            {
                return;
            }

            origin = newOrigin;

            if (showGrid)
                PrintGrid();
        }

        private void ChangeCellSize(float newCellWidth, float newCellHeight)
        {
            if (newCellWidth <= 0 || newCellHeight <= 0)
            {
                return;
            }
            if (newCellWidth == cellWidth && newCellHeight == cellHeight)
            {
                return;
            }

            cellWidth = newCellWidth;
            cellHeight = newCellHeight;

            if (showGrid)
                PrintGrid();
        }

        private void InitializeGridPrint()
        {
            // Create canvas for debug drawing
            canvasGO = new GameObject("Debug_Canvas_GRID2D");
            canvasGO.AddComponent<Canvas>();

            // Create text objects for debug printing
            debugTextGrid = new GameObject[width, height];
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    GameObject txtGO = new GameObject("Debug_Text_GRID2D");
                    txtGO.transform.parent = canvasGO.transform;

                    txtGO.AddComponent<TextMeshProUGUI>();

                    debugTextGrid[i, j] = txtGO;
                }
            }
        }

        private void PrintGrid()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Vector2 cellPos = GetCelldPos(i, j);

                    PrintCellLine(cellPos);
                    PrintCellContent(i, j, cellPos);
                }
            }

            // Draw top and right line of the grid
            Debug.DrawLine(new Vector2(origin.x, origin.y + (height * cellHeight)),
                new Vector2(origin.x + (width * cellWidth), origin.y + (height * cellHeight)), Color.black, 0.01f);
            Debug.DrawLine(new Vector2(origin.x + (width * cellWidth), origin.y),
                new Vector2(origin.x + (width * cellWidth), origin.y + (height * cellHeight)), Color.black, 0.01f);
        }

        private void PrintCellLine(Vector2 pos)
        {
            Vector2 start = new Vector2(pos.x - (cellWidth / 2), pos.y - (cellHeight / 2));
            Debug.DrawLine(start, new Vector2(start.x + cellWidth, start.y), Color.black, 0.01f);
            Debug.DrawLine(start, new Vector2(start.x, start.y + cellHeight), Color.black, 0.01f);
        }

        private void PrintCellContent(int i, int j, Vector2 pos)
        {
            GameObject txtGO = debugTextGrid[i, j];
            txtGO.transform.position = GetCelldPos(i, j);

            TextMeshProUGUI txt = txtGO.GetComponent<TextMeshProUGUI>();
            txt.fontSize = cellWidth / 4;
            txt.color = Color.black;
            txt.alignment = TextAlignmentOptions.Midline;

            if (grid[i, j] == null)
            {
                txt.text = "null";
            }
            else
            {
                txt.text = grid[i, j].ToString();
            }
        }
        #endregion
    }
}
