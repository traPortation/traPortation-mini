using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BoardElements {
    public class MultiEdge : IEdge {
        public Node From { get; }
        public Node To { get; }
        public float Cost { get; }
        private List<Edge> edges;
        public IReadOnlyList<Edge> Edges {
            get { return this.edges; }
        }
        public MultiEdge(List<Edge> edges) {
            if (edges.Count == 0) throw new System.Exception("MultiEdge needs 1 Edge at least");
            this.From = edges.First().From;
            this.To = edges.Last().To;
            this.Cost = edges.Aggregate(0f, (acc, edge) => acc + edge.Cost);
            this.edges = edges;
        }
    }
}