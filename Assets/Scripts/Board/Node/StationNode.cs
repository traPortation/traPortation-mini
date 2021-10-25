using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 駅付きのNode
    /// </summary>
    public class StationNode : IIndexedNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<IIndexedEdge> edges { get; }
        public IReadOnlyList<IIndexedEdge> Edges => this.edges;

        public readonly Station Station;
        public StationNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.edges = new List<IIndexedEdge>();
        }
        public void AddEdge(IIndexedEdge edge)
        {
            this.edges.Add(edge);
        }
    }
}

