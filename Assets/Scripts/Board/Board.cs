using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;
using Const;

#nullable enable

/// <summary>
/// 盤面 頂点 (Node) と辺 (Edge) によって構成される
/// </summary>
public class Board : Singleton<Board>
{
    private List<IBoardNode> nodes;
    public IReadOnlyList<IBoardNode> Nodes
    {
        get { return this.nodes; }
    }
    public Board()
    {
        this.nodes = new List<IBoardNode>();
    }

    /// <summary>
    /// BoardにStationNodeを追加する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>追加されたNode</returns>
    public StationNode AddStationNode(float x, float y)
    {
        // TODO: 画面外への設置は弾く
        var node = new StationNode(x, y, this.nodes.Count);
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
        if (from == null || to == null) throw new System.Exception("node is null");

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
    public RoadEdge<T, IBoardNode> AddRoadEdge<T>(T from, IBoardNode to)
        where T : IBoardNode, IRoadAddableNode<T>
    {
        if (from == null || to == null) throw new System.Exception("node is null");

        float cost = EdgeCost.Get(EdgeType.Walk) * Utils.Node.Distance(from, to);
        var edge = from.AddRoad(to, cost);
        return edge;
    }

    /// <summary>
    /// 経路探索を行う
    /// メモリと相談だけど出発点ごとにメモ化したほうがいいかも
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns>最短経路</returns>
    private List<IIndexedEdge> dijkstra(IIndexedNode start, IIndexedNode goal)
    {
        var from = Enumerable.Repeat<IIndexedEdge?>(null, this.Nodes.Count).ToList();

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
                    case IIndexedEdge e:
                        if (dist[e.To.Index] <= nextCost) continue;
                        dist[e.To.Index] = nextCost;
                        from[e.To.Index] = e;
                        que.Push((dist[e.To.Index], e.To.Index));
                        break;
                    default:
                        // unreachableなはず
                        throw new System.Exception();
                }

            }
        }
        var edges = new List<IIndexedEdge>();

        // startからgoalへの道がない場合は例外を投げる (仮仕様)
        if (dist[goal.Index] == float.MaxValue)
        {
            throw new System.Exception("path not found");
        }


        for (var cur = from[goal.Index]; cur != null; cur = from[cur.From.Index])
        {
            edges.Add(cur);
        }

        edges.Reverse();

        return edges;
    }
    /// <summary>
    /// 経路を取得する
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public Path GetPath(IIndexedNode start, IIndexedNode goal)
    {
        var edges = dijkstra(start, goal);
        return new Path(edges);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
