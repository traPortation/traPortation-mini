using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    public class Vertex
    {
        public float X { get; }
        public float Y { get; }
        public List<Edge> Edges { get; }
        public int Index { get; }
        // コンストラクタ
        public Vertex(float x, float y, int index) {
            this.X = x;
            this.Y = y;
            this.Edges = new List<Edge>();
            this.Index = index;
        }
    }
}