using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TetrisInventoryTC;

public class TestGrid : MonoBehaviour
{
    Grid2D<int> grid;

    [Range(0, 20f)] public float cellWidth;
    [Range(0, 20f)] public float cellHeight;
    [Range(0, 20)] public int width;
    [Range(0, 20)] public int height;

    private void Awake()
    {
        grid = new Grid2D<int>(true);
    }

    private void Update()
    {
        grid.FollowChangedValues(width, height, transform.position, cellWidth, cellHeight);

        if (Input.GetMouseButtonDown(0))
        {
            
            grid.SetCellValue(Input.mousePosition, grid.GetCellValue(Input.mousePosition) + 1);
        }
    }
}
