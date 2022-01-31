using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisInventoryTC
{
    // TODO - Make an enum for shapes

    [CreateAssetMenu(fileName = "Tetris_Item", menuName = "Tetris_Item")]
    public class TetrisItem : ScriptableObject
    {
        [SerializeField] new string name;

        public override string ToString()
        {
            return name;
        }
    }
}
