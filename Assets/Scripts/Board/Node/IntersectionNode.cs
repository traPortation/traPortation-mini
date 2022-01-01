using System.Collections.Generic;

namespace BoardElements
{
    /// <summary>
    /// 交差点
    /// </summary>
    public class IntersectionNode : IBoardNode, IRoadAddableNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        protected List<RoadEdge<IBoardNode>> roads { get; }
        public virtual IReadOnlyList<IEdge<IBoardNode>> Edges => this.roads;
        public IntersectionNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.roads = new List<RoadEdge<IBoardNode>>();
        }
        public RoadEdge<IBoardNode> AddRoad(IBoardNode toNode, float cost)
        {
            var edge = new RoadEdge<IBoardNode>(toNode, cost);
            this.roads.Add(edge);
            return edge;
        }
    }
}