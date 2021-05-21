using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    public class Node
    {
        public float X { get; }
        public float Y { get; }
        public Node(float x, float y) {
            this.X = x;
            this.Y = y;
        }
        public static float Distance(Node a, Node b) {
            return Mathf.Sqrt(Mathf.Pow(a.X - b.X, 2) + Mathf.Pow(a.Y - b.Y, 2));
        }
    }
}