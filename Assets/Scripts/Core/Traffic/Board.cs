using System.Collections.Generic;
using System.Linq;
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
        /// <param name="type">EdgeTypeから指定する</param>
        /// <returns>追加されたEdge</returns>
        public VehicleEdge AddVehicleRoute(StationNode from, StationNode to, EdgeType type)
        {
            float cost = Const.EdgeCost.Get(type) * Utils.Node.Distance(from, to);
            var edge = from.AddVehicleRoute(to, cost);
            return edge;
        }

        public VehicleEdge AddVehicleRoute(StationNode from, StationNode to, float cost)
        {
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
            float cost = Const.EdgeCost.Get(EdgeType.Walk) * Utils.Node.Distance(from, to);
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
        private List<INode> searchPath(List<(float, IIndexedNode)> starts, List<(float, IIndexedNode)> goals)
        {
            var from = Enumerable.Repeat<(IIndexedNode?, IEdge<IBoardNode, IBoardNode>?)>((null, null), this.Nodes.Count).ToList();

            var dist = Enumerable.Repeat<float>(float.MaxValue, this.Nodes.Count).ToList();
            var que = new Utils.PriorityQueue<(float, int)>(this.Nodes.Count + 10);

            foreach (var (cost, start) in starts)
            {
                dist[start.Index] = cost;
                que.Push((cost, start.Index));
            }

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

            var goal = goals[0].Item2;
            var minDist = dist[goals[0].Item2.Index] + goals[0].Item1;
            foreach (var g in goals)
            {
                if (minDist > dist[g.Item2.Index] + g.Item1)
                {
                    minDist = dist[g.Item2.Index] + g.Item1;
                    goal = g.Item2;
                }
            }

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
            var (startRoad, startPoint) = this.GetNearestRoad(new Vector2(start.X, start.Y));
            var (goalRoad, goalPoint) = this.GetNearestRoad(new Vector2(goal.X, goal.Y));

            if (startRoad == null || goalRoad == null)
            {
                throw new System.Exception("road not found");
            }

            var startFromCost = Utils.Node.Distance(start, startRoad.From) * Const.EdgeCost.Get(EdgeType.Walk);
            var startToCost = Utils.Node.Distance(start, startRoad.To) * Const.EdgeCost.Get(EdgeType.Walk);
            var starts = new List<(float, IIndexedNode)>() { (startFromCost, startRoad.From), (startToCost, startRoad.To) };

            var goalFromCost = Utils.Node.Distance(goal, goalRoad.From) * Const.EdgeCost.Get(EdgeType.Walk);
            var goalToCost = Utils.Node.Distance(goal, goalRoad.To) * Const.EdgeCost.Get(EdgeType.Walk);
            var goals = new List<(float, IIndexedNode)>() { (goalFromCost, goalRoad.From), (goalToCost, goalRoad.To) };

            var searchedNodes = this.searchPath(starts, goals);

            // TODO: 効率がよくないので改善する
            searchedNodes.Insert(0, new TemporaryNode(startPoint.x, startPoint.y));
            searchedNodes.Insert(0, start);
            searchedNodes.Add(new TemporaryNode(goalPoint.x, goalPoint.y));
            searchedNodes.Add(goal);

            return searchedNodes;
        }

        public List<IBoardNode> SearchRoad(IBoardNode start, IBoardNode goal)
        {
            var from = Enumerable.Repeat<(IBoardNode?, IEdge<IBoardNode, IBoardNode>?)>((null, null), this.Nodes.Count).ToList();

            var dist = Enumerable.Repeat<float>(float.MaxValue, this.Nodes.Count).ToList();
            var que = new Utils.PriorityQueue<(float, int)>();
            que.Push((0, start.Index));
            dist[start.Index] = 0;

            while (que.Count != 0)
            {
                var (cost, idx) = que.Top;
                que.Pop();
                if (dist[idx] < cost) continue;

                // 道のみを探索
                foreach (var edge in this.Nodes[idx].Roads)
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
            var nodes = new List<IBoardNode>();

            // startからgoalへの道がない場合は例外を投げる (仮仕様)
            if (dist[goal.Index] == float.MaxValue)
            {
                throw new System.Exception("path not found");
            }

            nodes.Add(goal);
            int i = 0;
            for (var cur = from[goal.Index]; cur.Item1 != null; cur = from[cur.Item1.Index])
            {
                nodes.Add(cur.Item1);
                i++;
                if (i > this.Nodes.Count)
                {
                    throw new System.Exception("loop detected");
                }
            }

            nodes.Reverse();

            return nodes;
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
            var x = Random.Range(Const.X.Min, Const.X.Max);
            var y = Random.Range(Const.Y.Min, Const.Y.Max);

            return new TemporaryNode(x, y);
        }
    }
}