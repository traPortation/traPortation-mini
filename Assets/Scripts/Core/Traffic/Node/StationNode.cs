using System.Collections.Generic;
using System.Linq;
using TraPortation.Traffic.Edge;

#nullable enable

namespace TraPortation.Traffic.Node
{
    /// <summary>
    /// 駅付きのNode
    /// </summary>
    public class StationNode : IntersectionNode
    {
        List<VehicleEdge> vehicleRoutes { get; }
        public override IEnumerable<IEdge<IBoardNode, IBoardNode>> Edges
        {
            get
            {
                var roads = this.roads as IEnumerable<IEdge<IBoardNode, IBoardNode>>;
                var routes = this.vehicleRoutes as IEnumerable<IEdge<IBoardNode, IBoardNode>>;

                return roads.Concat(routes);
            }
        }
        public readonly StationKind Kind;
        public StationNode(float x, float y, int index, StationKind kind) : base(x, y, index)
        {
            this.vehicleRoutes = new List<VehicleEdge>();
            this.Kind = kind;
        }

        public VehicleEdge AddVehicleRoute(StationNode to, float cost)
        {
            var edge = new VehicleEdge(to, cost);
            this.vehicleRoutes.Add(edge);
            return edge;
        }
    }
}

