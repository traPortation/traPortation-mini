using System.Collections.Generic;

namespace BoardElements
{
    /// <summary>
    /// 交差点
    /// </summary>
    public class IntersectionNode : IBoardNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        protected List<RoadEdge> roads { get; }
        public virtual IEnumerable<IEdge<IBoardNode, IBoardNode>> Edges => this.roads;
        public IntersectionNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.roads = new List<RoadEdge>();
        }
        public RoadEdge AddRoad(IBoardNode toNode, float cost)
        {
            var edge = new RoadEdge(this, toNode, cost);
            this.roads.Add(edge);
            return edge;
        }
    }
}