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
        public override IReadOnlyList<IEdge<IBoardNode>> Edges
        {
            get
            {
                var roads = this.roads as IEnumerable<IEdge<IBoardNode>>;
                var routes = this.vehicleRoutes as IEnumerable<IEdge<IBoardNode>>;

                // TODO: ToListが必要か考える
                // そもそもIEnumerableで受け取れるようにする / roadsとroutesを別々に受け取るようにする など
                return roads.Concat(routes).ToList();
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

