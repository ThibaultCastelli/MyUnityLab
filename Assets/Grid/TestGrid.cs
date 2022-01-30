using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid2DTC;

public class TestGrid : MonoBehaviour
{
    Grid2D<bool> grid;

    [Range(0, 20f)] public float cellWidth;
    [Range(0, 20f)] public float cellHeight;
    [Range(0, 20)] public int width;
    [Range(0, 20)] public int height;

    private void Awake()
    {
        grid = new Grid2D<bool>(3, 3, new Vector2(0, 0), 10, 10, true);
    }

    private void Update()
    {
        grid.FollowChangedValues(width, height, transform.position, cellWidth, cellHeight);
    }
}
