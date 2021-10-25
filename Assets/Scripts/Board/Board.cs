using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;
using Collections;
using Const;

/// <summary>
/// 盤面 頂点 (Node) と辺 (Edge) によって構成される
/// </summary>
public class Board : Singleton<Board>
{
    private List<IIndexedNode> nodes;
    public IReadOnlyList<IIndexedNode> Nodes
    {
        get { return this.nodes; }
    }
    public Board()
    {
        this.nodes = new List<IIndexedNode>();
    }

    static float nodeDistance(INode a, INode b)
    {
        return Mathf.Sqrt(Mathf.Pow(a.X - b.X, 2) + Mathf.Pow(a.Y - b.Y, 2));
    }

    /// <summary>
    /// BoardにEdgeを追加する
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="type">Const.EdgeTypeから指定する</param>
    /// <returns>追加されたEdge</returns>
    public VehicleEdge AddStationEdge(StationNode from, StationNode to, EdgeType type = EdgeType.Walk)
    {
        float cost = EdgeCost.Get(type) * Board.nodeDistance(from, to);
        var edge = new VehicleEdge(from, to, cost);
        if (edge == null) throw new System.Exception("edge is null");
        if (edge.From == null || edge.To == null) throw new System.Exception("node is null");
        from.AddEdge(edge);
        return edge;
    }
    /// <summary>
    /// BoardにNodeを追加する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>追加されたNode</returns>
    public StationNode AddNode(float x, float y)
    {
        var node = new StationNode(x, y, this.nodes.Count);
        this.nodes.Add(node);
        if (node == null) throw new System.Exception("node is null");
        return node;
    }

    /// <summary>
    /// 経路探索を行う
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns>最短経路</returns>
    private List<IEdge> dijkstra(INode start, INode goal)
    {
        var fromStart = new List<MultiEdge>();
        var toGoal = new List<MultiEdge>();

        foreach (var node in this.nodes)
        {
            fromStart.Add(multiPathBetweenNodes(start, node));
            toGoal.Add(multiPathBetweenNodes(node, goal));
        }

        var from = Enumerable.Repeat<IIndexedEdge>(null, this.Nodes.Count).ToList();
        var que = new PriorityQueue<(float, int)>();

        var dist = this.nodes.Select(node =>
        {
            var edge = multiPathBetweenNodes(start, node);
            fromStart.Add(edge);
            que.Push((edge.Cost, node.Index));
            return edge.Cost;
        }).ToList();

        int lastIndex = -1;
        MultiEdge lastEdge = multiPathBetweenNodes(start, goal);
        var goalCost = lastEdge.Cost;

        while (que.Count != 0)
        {
            var (cost, idx) = que.Top;
            que.Pop();
            if (dist[idx] < cost) continue;
            foreach (var edge in this.Nodes[idx].Edges)
            {
                var nextCost = cost + edge.Cost;
                if (dist[edge.To.Index] <= nextCost) continue;
                dist[edge.To.Index] = nextCost;
                from[edge.To.Index] = edge;
                que.Push((dist[edge.To.Index], edge.To.Index));
            }
            if (cost + toGoal[idx].Cost < goalCost)
            {
                lastIndex = idx;
                lastEdge = toGoal[idx];
                goalCost = cost + toGoal[idx].Cost;
            }
        }
        var edges = new List<IEdge>();

        if (lastIndex != -1)
        {
            for (var cur = from[lastIndex]; cur != null; cur = from[cur.From.Index])
            {
                edges.Add(cur);
                if (from[cur.From.Index] == null)
                {
                    var bef = edges.Count;
                    var startEdge = fromStart[cur.From.Index].Edges.AsEnumerable().Reverse();
                    // edges.Concat(startEdge); がなんか上手くいかない
                    foreach (var edge in startEdge) edges.Add(edge);
                }
            }
        }
        edges.Reverse();
        // 同上
        foreach (var edge in lastEdge.Edges) edges.Add(edge);
        return edges;
    }
    /// <summary>
    /// 経路を取得する
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public Path GetPath(INode start, INode goal)
    {
        var edges = dijkstra(start, goal);
        return new Path(edges);
    }
    /// <summary>
    /// 2点間の歩く経路をMultiEdgeとして返す
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    private MultiEdge multiPathBetweenNodes(INode origin, INode destination)
    {
        Node getRoad(INode position)
        { // ある点がそこから一番近い道にでるための関数、ある点が道上ならそのまま返す（引数：任意の点）
            float leftSideRoad = position.X % 1;
            float downSideRoad = position.Y % 1;
            float rightSideRoad = 1f - leftSideRoad;
            float upSideRoad = 1f - downSideRoad;

            float minRoad = new float[] { leftSideRoad, downSideRoad, rightSideRoad, upSideRoad }.Min();

            if (minRoad == rightSideRoad) return new Node(Mathf.Ceil(position.X), position.Y);
            else if (minRoad == upSideRoad) return new Node(position.X, Mathf.Ceil(position.Y));
            else if (minRoad == leftSideRoad) return new Node(Mathf.Floor(position.X), position.Y);
            else return new Node(position.X, Mathf.Floor(position.Y));
        }

        Node getIntersection(Node position)
        { // ある点が目的地から一番近い交差点に出るための関数(引数:道上の点)
            if (position.X == (int)position.X && position.Y != (int)position.Y)
            { // x座標整数なら
                if (position.Y >= destination.Y) return new Node(position.X, Mathf.Floor(position.Y));
                else return new Node(position.X, Mathf.Ceil(position.Y));
            }
            else if (position.X != (int)position.X && position.Y == (int)position.Y)
            { // y座標整数なら
                if (position.X >= destination.X) return new Node(Mathf.Floor(position.X), position.Y);
                else return new Node(Mathf.Ceil(position.X), position.Y);
            }
            else
            { // 交差点上（or道上じゃないとき）
                return new Node((int)position.X, (int)position.Y);
            }
        }

        Node originToRoad = getRoad(origin), destinationToRoad = getRoad(destination); // origin, destination から道に出た時のそれぞれの座標

        // 碁盤の目状でランダムに進むための準備
        Node gobanFirst = getIntersection(originToRoad), gobanLast = getIntersection(destinationToRoad);
        int xMoving = (int)(gobanLast.X - gobanFirst.X), yMoving = (int)(gobanLast.Y - gobanFirst.Y); // gobanFirst から gobanLast まで x 方向、y 方向にいくら進むか。
        int n = Mathf.Abs(xMoving) + Mathf.Abs(yMoving) + 1; // 移動量

        var path = new List<INode>(); // 返り値
        path.Add(origin);
        if (origin != originToRoad) path.Add(originToRoad); // origin が道の上でないなら
        if (gobanFirst != origin) path.Add(gobanFirst); // origin が交差点上でないなら

        int plusOrMinusX = xMoving != 0 ? xMoving / Mathf.Abs(xMoving) : 1;
        int plusOrMinusY = yMoving != 0 ? yMoving / Mathf.Abs(yMoving) : 1; // gobanFirst から gobanLast までの各軸の移動の方向を表す

        // randomPath の生成
        while (xMoving != 0 || yMoving != 0)
        {
            var last = path.Last();
            if (xMoving != 0 && yMoving != 0)
            { // x 軸方向とy 軸方向共に移動可能か
                int rnd = Random.Range(0, 2); // 0 なら x 軸方向へ、1 なら y 軸方向へ
                if (rnd == 0)
                {
                    path.Add(new Node(last.X + plusOrMinusX, last.Y));
                    xMoving = xMoving - plusOrMinusX;
                }
                else
                {
                    path.Add(new Node(last.X, last.Y + plusOrMinusY));
                    yMoving = yMoving - plusOrMinusY;
                }
            }
            else
            {
                if (xMoving == 0)
                { // ずっと y 軸方向へ
                    path.Add(new Node(last.X, last.Y + plusOrMinusY));
                    yMoving = yMoving - plusOrMinusY;
                }
                else if (yMoving == 0)
                { // ずっと x 軸方向へ
                    path.Add(new Node(last.X + plusOrMinusX, last.Y));
                    xMoving = xMoving - plusOrMinusX;
                }
            }
        }

        if (gobanLast != destination) path.Add(destinationToRoad); // destination が交差点上でないなら
        if (destinationToRoad != destination) path.Add(destination); // destination が道の上でないなら

        var edges = new List<IEdge>();
        for (int i = 0; i < path.Count - 1; i++)
        {
            INode from = path[i];
            INode to = path[i + 1];
            float cost = Board.nodeDistance(from, to) * EdgeCost.Get(EdgeType.Walk);
            edges.Add(new Edge(from, to, cost, EdgeType.Walk));
        }
        return new MultiEdge(edges);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
