using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HeatMapVisual : MonoBehaviour
    {
        [HideInInspector] public GridMap grid;

        Mesh mesh;
        MeshFilter meshFilter;

        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        private void Awake()
        {
            mesh = new Mesh();
            meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            grid.OnGridValueChanged += UpdateHeatMapVisual;
            vertices = new Vector3[4 * grid.Width * grid.Height];
            uv = new Vector2[4 * grid.Width * grid.Height];
            triangles = new int[6 * grid.Width * grid.Height];

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    int index = x * grid.Height + y;
                    float uvValue = (float)grid.GetValue(x, y) / grid.MaxCellValue;
                    Vector3 currentWorldPos = grid.GetWorldPos(x, y);

                    vertices[index * 4] = currentWorldPos;
                    vertices[index * 4 + 1] = new Vector3(currentWorldPos.x, currentWorldPos.y + grid.CellSize);
                    vertices[index * 4 + 2] = new Vector3(currentWorldPos.x + grid.CellSize, currentWorldPos.y + grid.CellSize);
                    vertices[index * 4 + 3] = new Vector3(currentWorldPos.x + grid.CellSize, currentWorldPos.y);

                    uv[index * 4] = new Vector2(uvValue, 0);
                    uv[index * 4 + 1] = new Vector2(uvValue, 0);
                    uv[index * 4 + 2] = new Vector2(uvValue, 0);
                    uv[index * 4 + 3] = new Vector2(uvValue, 0);

                    triangles[index * 6] = index * 4;
                    triangles[index * 6 + 1] = index * 4 + 1;
                    triangles[index * 6 + 2] = index * 4 + 2;
                    triangles[index * 6 + 3] = index * 4;
                    triangles[index * 6 + 4] = index * 4 + 2;
                    triangles[index * 6 + 5] = index * 4 + 3;
                }
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            meshFilter.mesh = mesh;
        }

        public void UpdateHeatMapVisual(int[,] grid, int x, int y)
        {
            int index = x * this.grid.Height + y;
            float uvValue = (float)this.grid.GetValue(x, y) / this.grid.MaxCellValue;

            uv[index * 4] = new Vector2(uvValue, 0);
            uv[index * 4 + 1] = new Vector2(uvValue, 0);
            uv[index * 4 + 2] = new Vector2(uvValue, 0);
            uv[index * 4 + 3] = new Vector2(uvValue, 0);

            mesh.uv = uv;
            meshFilter.mesh = mesh;
        }
    }
}