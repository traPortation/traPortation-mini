using System.Collections.Generic;

namespace BoardElements
{
    public class IntersectionNode : IBoardNode, IRoadAddableNode<IntersectionNode>
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<RoadEdge<IntersectionNode, IBoardNode>> roads { get; }
        public IReadOnlyList<IEdge<IBoardNode, IBoardNode>> Edges => this.roads;
        public IntersectionNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.roads = new List<RoadEdge<IntersectionNode, IBoardNode>>();
        }
        public RoadEdge<IntersectionNode, IBoardNode> AddRoad(IBoardNode toNode, float cost)
        {
            var edge = new RoadEdge<IntersectionNode, IBoardNode>(this, toNode, cost);
            this.roads.Add(edge);
            return edge;
        }
    }
}