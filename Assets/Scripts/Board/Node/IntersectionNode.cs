using System.Collections.Generic;

namespace BoardElements
{
    public class IntersectionNode : IIndexedNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<IIndexedEdge> edges { get; }
        public IReadOnlyList<IIndexedEdge> Edges => this.edges;

        public IntersectionNode(float x, float y, int index)
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