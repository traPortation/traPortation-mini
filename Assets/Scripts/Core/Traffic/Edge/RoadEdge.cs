using TraPortation.Traffic.Node;
using UnityEngine;

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

        public (float, Vector2) DistanceToPoint(Vector2 vec)
        {
            var from = new Vector2(this.From.X, this.From.Y);
            var to = new Vector2(this.To.X, this.To.Y);

            if (Vector2.Dot(to - from, vec - from) < 0)
            {
                return (Vector2.Distance(vec, from), from);
            }
            if (Vector2.Dot(from - to, vec - to) < 0)
            {
                return (Vector2.Distance(vec, to), to);
            }

            var point = Vector2.Dot(to - from, vec - from) / Vector2.Dot(to - from, to - from) * (to - from) + from;
            return (Vector2.Distance(vec, point), point);
        }
    }
}
