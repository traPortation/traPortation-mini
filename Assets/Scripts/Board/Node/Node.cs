using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// ただのNode
    /// 将来的に使わなくなるかも
    /// </summary>
    class Node : INode
    {
        public float X { get; }
        public float Y { get; }
        public Node(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}