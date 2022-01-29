using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

namespace BoardElements
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
        // TODO: stationをsetする
        public readonly Station Station;
        public StationNode(float x, float y, int index): base(x, y, index)
        {
            this.vehicleRoutes = new List<VehicleEdge>();
        }

        public VehicleEdge AddVehicleRoute(StationNode to, float cost)
        {
            var edge = new VehicleEdge(to, cost);
            this.vehicleRoutes.Add(edge);
            return edge;
        }
    }
}

