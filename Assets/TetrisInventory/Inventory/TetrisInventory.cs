using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisInventoryTC
{
    public class TetrisInventory : MonoBehaviour
    {
        private Grid2D<TetrisItem> inventory;

        [SerializeField] TetrisItem defaultItem;

        [SerializeField] [Range(0, 20)] int width = 5;
        [SerializeField] [Range(0, 20)] int height = 5;
        [SerializeField] [Range(0, 20f)] float cellWidth = 2;
        [SerializeField] [Range(0, 20f)] float cellHeight = 2;

        private void Awake()
        {
            inventory = new Grid2D<TetrisItem>(width, height, transform.position, cellWidth, cellHeight, true);
        }

        private void Update()
        {
            inventory.FollowChangedValues(width, height, transform.position, cellWidth, cellHeight);

            if (Input.GetMouseButtonDown(0))
            {
                inventory.SetCellValue(Input.mousePosition, defaultItem);
            }
        }
    }
}
