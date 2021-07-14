using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// Board上のNodeを表す
    /// </summary>
    public class BoardNode : Node
    {
        public readonly int Index;
        private List<BoardEdge> edges;
        public IReadOnlyList<BoardEdge> Edges => this.edges;
        public BoardNode(float x, float y, int index) : base(x, y)
        {
            this.Index = index;
            this.edges = new List<BoardEdge>();
        }
        public void AddEdge(BoardEdge edge)
        {
            this.edges.Add(edge);
        }
    }
}

