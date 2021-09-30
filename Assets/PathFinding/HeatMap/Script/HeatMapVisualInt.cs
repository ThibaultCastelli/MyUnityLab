using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindingTC
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HeatMapVisualInt : MonoBehaviour
    {
        [HideInInspector] public GridMap<int> grid;

        Mesh mesh;
        MeshFilter meshFilter;

        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;

        bool updateHeatMap;

        private void Awake()
        {
            mesh = new Mesh();
            meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            grid.OnGridValueChanged += (int x, int y) => updateHeatMap = true;

            // Set the meshes to default value
            vertices = new Vector3[4 * grid.Width * grid.Height];
            uv = new Vector2[4 * grid.Width * grid.Height];
            triangles = new int[6 * grid.Width * grid.Height];

            // Place the meshes on the grid
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    int index = x * grid.Height + y;
                    float uvValue = 0;      // value / maxValue
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

        private void LateUpdate()
        {
            // Update the heat map only at the end of the frame
            if (updateHeatMap)
            {
                updateHeatMap = false;
                UpdateHeatMapVisual();
            }
        }

        public void UpdateHeatMapVisual()
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    int index = x * grid.Height + y;
                    float uvValue = 0;      // value / maxValue

                    uv[index * 4] = new Vector2(uvValue, 0);
                    uv[index * 4 + 1] = new Vector2(uvValue, 0);
                    uv[index * 4 + 2] = new Vector2(uvValue, 0);
                    uv[index * 4 + 3] = new Vector2(uvValue, 0);
                }
            }

            mesh.uv = uv;
            meshFilter.mesh = mesh;
        }
    }
}