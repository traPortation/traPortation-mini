using System.Collections.Generic;
using System.Linq;
using TraPortation.Const;
using TraPortation.Traffic.Edge;
using TraPortation.Traffic.Node;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation.Traffic
{
    /// <summary>
    /// 盤面 頂点 (Node) と辺 (Edge) によって構成される
    /// </summary>
    public class Board
    {
        List<IntersectionNode> nodes;
        public IReadOnlyList<IntersectionNode> Nodes => this.nodes;
        List<RoadEdge> roads;

        [Inject]
        public Board()
        {
            this.nodes = new List<IntersectionNode>();
            this.roads = new List<RoadEdge>();
        }

        /// <summary>
        /// BoardにStationNodeを追加する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>追加されたNode</returns>
        public StationNode AddStationNode(float x, float y, StationKind kind)
        {
            // TODO: 画面外への設置は弾く
            var node = new StationNode(x, y, this.nodes.Count, kind);
            this.nodes.Add(node);
            return node;
        }

        /// <summary>
        /// BoardにIntersectionNodeを追加する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>追加されたNode</returns>
        public IntersectionNode AddIntersectionNode(float x, float y)
        {
            var node = new IntersectionNode(x, y, this.nodes.Count);
            this.nodes.Add(node);
            return node;
        }

        /// <summary>
        /// BoardにVehicleEdgeを追加する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="type">Const.EdgeTypeから指定する</param>
        /// <returns>追加されたEdge</returns>
        public VehicleEdge AddVehicleRoute(StationNode from, StationNode to, EdgeType type = EdgeType.Walk)
        {
            float cost = EdgeCost.Get(type) * Utils.Node.Distance(from, to);
            var edge = from.AddVehicleRoute(to, cost);
            return edge;
        }

        /// <summary>
        /// RoadEdgeを作成して追加する
        /// </summary>
        /// <param name="from">始点</param>
        /// <param name="to">終点</param>
        /// <typeparam name="T">始点の型</typeparam>
        /// <returns>作成したRoadEdge</returns>
        public RoadEdge AddRoadEdge(IBoardNode from, IBoardNode to)
        {
            float cost = EdgeCost.Get(EdgeType.Walk) * Utils.Node.Distance(from, to);
            var edge = from.AddRoad(to, cost);
            this.roads.Add(edge);
            return edge;
        }

        /// <summary>
        /// 経路探索を行う
        /// メモリと相談だけど出発点ごとにメモ化したほうがいいかも
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <returns>最短経路</returns>
        private List<INode> searchPath(IIndexedNode start, IIndexedNode goal)
        {
            var from = Enumerable.Repeat<(IIndexedNode?, IEdge<IBoardNode, IBoardNode>?)>((null, null), this.Nodes.Count).ToList();

            var dist = Enumerable.Repeat<float>(float.MaxValue, this.Nodes.Count).ToList();
            dist[start.Index] = 0;

            var que = new Utils.PriorityQueue<(float, int)>();
            que.Push((0, start.Index));

            while (que.Count != 0)
            {
                var (cost, idx) = que.Top;
                que.Pop();
                if (dist[idx] < cost) continue;


                foreach (var edge in this.Nodes[idx].Edges)
                {
                    var nextCost = cost + edge.Cost;
                    switch (edge)
                    {
                        case IEdge<IBoardNode, IBoardNode> e:
                            if (dist[e.To.Index] <= nextCost) continue;
                            dist[e.To.Index] = nextCost;
                            from[e.To.Index] = (this.Nodes[idx], e);
                            que.Push((dist[e.To.Index], e.To.Index));
                            break;
                        default:
                            // unreachableなはず
                            throw new System.Exception();
                    }

                }
            }
            var nodes = new List<INode>();

            // startからgoalへの道がない場合は例外を投げる (仮仕様)
            if (dist[goal.Index] == float.MaxValue)
            {
                throw new System.Exception("path not found");
            }

            nodes.Add(goal);
            for (var cur = from[goal.Index]; cur.Item1 != null; cur = from[cur.Item1.Index])
            {
                nodes.Add(cur.Item1);
            }

            nodes.Reverse();

            return nodes;
        }
        /// <summary>
        /// 経路を取得する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        public IReadOnlyList<INode> GetPath(INode start, INode goal)
        {
            var startIntersection = this.GetNearestNode(start.X, start.Y);
            var goalIntersection = this.GetNearestNode(goal.X, goal.Y);

            var searchedNodes = searchPath(startIntersection, goalIntersection);

            // TODO: 効率がよくないので改善する
            searchedNodes.Insert(0, start);
            searchedNodes.Add(goal);

            return searchedNodes;
        }

        public IntersectionNode GetNearestNode(float x, float y)
        {
            return this.nodes.OrderBy(node => Mathf.Pow(node.X - x, 2) + Mathf.Pow(node.Y - y, 2)).First();
        }

        public (RoadEdge?, Vector2) GetNearestRoad(Vector2 vec)
        {
            float dist = float.MaxValue;
            RoadEdge? nearest = null;
            Vector2 nearestPos = Vector2.zero;

            foreach (var r in this.roads)
            {
                var (d, pos) = r.DistanceToPoint(vec);
                if (d < dist)
                {
                    dist = d;
                    nearest = r;
                    nearestPos = pos;
                }
            }

            return (nearest, nearestPos);
        }

        public TemporaryNode GetRandomPoint()
        {
            var x = Random.Range(X.Min, X.Max);
            var y = Random.Range(Y.Min, Y.Max);

            return new TemporaryNode(x, y);
        }

        public void Test()
        {
            Debug.Log("Test");
        }
    }
}