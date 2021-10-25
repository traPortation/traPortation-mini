using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 駅付きのNode
    /// </summary>
    public class StationNode : INode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<Edge> edges { get; }
        public IReadOnlyList<IEdge> Edges => this.edges;

        public readonly Station Station;
        public StationNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.edges = new List<Edge>();
        }
        public void AddEdge(IEdge edge)
        {
            if (edge is Edge) this.edges.Add(edge as Edge);
        }
    }
}

