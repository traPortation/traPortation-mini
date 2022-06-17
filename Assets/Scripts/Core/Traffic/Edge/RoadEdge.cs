using TraPortation.Traffic.Node;

namespace TraPortation.Traffic.Edge
{
    /// <summary>
    /// 道
    /// </summary>
    public class RoadEdge : IEdge<IBoardNode, IBoardNode>
    {
        public IBoardNode From { get; }
        public IBoardNode To { get; }
        public float Cost { get; }
        public RoadEdge(IBoardNode from, IBoardNode to, float cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}
