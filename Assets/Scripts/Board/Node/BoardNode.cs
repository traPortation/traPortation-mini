using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements {
    public class BoardNode: Node
    {
        public int Index;
        private List<BoardEdge> edges;
        public IReadOnlyList<BoardEdge> Edges {
            get { return this.edges; }
        }
        public BoardNode(float x, float y, int index): base(x, y) {
            this.Index = index;
            this.edges = new List<BoardEdge>();
        }
        public void AddEdge(BoardEdge edge) {
            edges.Add(edge);
        }
    }
}

