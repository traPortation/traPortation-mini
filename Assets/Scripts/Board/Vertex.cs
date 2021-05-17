using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    public class Vertex
    {
        public float X { get; }
        public float Y { get; }
        public List<Edge> Edges{ get; }
        // コンストラクタ
        public Vertex(float x, float y) {
            this.X = x;
            this.Y = y;
            this.Edges = new List<Edge>();
        }
    }
}