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
    public class StationNode : IRoadAddableNode, IBoardNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<RoadEdge<IBoardNode>> roads { get; }
        List<VehicleEdge> vehicleRoutes { get; }
        public IReadOnlyList<IEdge<IBoardNode>> Edges
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
        public StationNode(float x, float y, int index)
        {
            this.X = x;
            this.Y = y;
            this.Index = index;
            this.roads = new List<RoadEdge<IBoardNode>>();
            this.vehicleRoutes = new List<VehicleEdge>();
        }
        public RoadEdge<IBoardNode> AddRoad(IBoardNode to, float cost)
        {
            var edge = new RoadEdge<IBoardNode>(to, cost);
            this.roads.Add(edge);
            return edge;
        }
        public VehicleEdge AddVehicleRoute(StationNode to, float cost)
        {
            var edge = new VehicleEdge(to, cost);
            this.vehicleRoutes.Add(edge);
            return edge;
        }
    }
}

