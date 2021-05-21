using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;
using Collections;
using Const;

public class Board
{
    private List<BoardNode> nodes;
    public IReadOnlyList<BoardNode> Nodes {
        get { return nodes; }
    }
    // コンストラクタ
    public Board() {
        this.nodes = new List<BoardNode>();
    }

    private float distance(Node from, Node to) {
        return Mathf.Sqrt(Mathf.Pow(from.X - to.X, 2) + Mathf.Pow(from.Y - to.Y, 2));
    }

    public void AddEdge(BoardNode from, BoardNode to, int type = EdgeType.Walk) {
        float cost = EdgeCost.Get(type) * distance(from, to);
        var edge = new BoardEdge(from, to, cost, type);
        if (edge == null) throw new System.Exception("edge is null");
        if (edge.To == null) throw new System.Exception("node is null"); 
        from.AddEdge(edge);
    }

    public BoardNode AddNode(float x, float y) {
        var node = new BoardNode(x, y, this.Nodes.Count);
        this.nodes.Add(node);
        if (node == null) throw new System.Exception("node is null");
        return node;
    }

    // start から goal への最短経路を探す
    private List<Edge> dijkstra(Node start, Node goal) {
        var fromStart = new List<MultiEdge>();
        var toGoal = new List<MultiEdge>();
        foreach (var node in nodes) {
            fromStart.Add(multiPathBetweenNodes(start, node));
            toGoal.Add(multiPathBetweenNodes(node, goal));
        }

        const float INF = float.MaxValue;
        var dist = Enumerable.Repeat<float>(INF, this.Nodes.Count).ToList();
        var from = Enumerable.Repeat<BoardEdge>(null, this.Nodes.Count).ToList();
        var que = new PriorityQueue<(float, int)>();
        
        for (int i = 0; i < fromStart.Count; i++) {
            dist[i] = fromStart[i].Cost;
            que.Push((dist[i], i));
        }

        int toGoalIndex = -1;
        MultiEdge toGoalEdge = multiPathBetweenNodes(start, goal);
        var toGoalCost = toGoalEdge.Cost;

        while (que.Count != 0) {
            var (cost, idx) = que.Top;
            que.Pop();
            if (dist[idx] < cost) continue;
            foreach (var edge in this.Nodes[idx].Edges) {
                var nextCost = cost + edge.Cost;
                if (dist[edge.To.Index] <= nextCost) continue;
                dist[edge.To.Index] = nextCost;
                from[edge.To.Index] = edge;
                que.Push((dist[edge.To.Index], edge.To.Index));
            }
            if (cost + toGoal[idx].Cost < toGoalCost) {
                toGoalIndex = idx;
                toGoalEdge = toGoal[idx];
                toGoalCost = cost + toGoal[idx].Cost;
            }
        }
        var edges = new List<Edge>();
        
        if (toGoalIndex != -1) {
            for (var cur = from[toGoalIndex]; cur!= null; cur = from[cur.From.Index]) {
                edges.Add(cur);
                if (from[cur.From.Index] == null) {
                    var bef = edges.Count;
                    var startEdge = fromStart[cur.From.Index].Edges.AsEnumerable().Reverse();
                    
                    // edges.Concat(startEdge); がなんか上手くいかない
                    foreach (var edge in startEdge) edges.Add(edge);
                }
            }
        }
        edges.Reverse();
        // 同上
        foreach (var edge in toGoalEdge.Edges) edges.Add(edge);
        return edges;
    }
    public Path GetPath(Node start, Node goal) {
        var edges = dijkstra(start, goal);
        return new Path(edges);
    }

    private MultiEdge multiPathBetweenNodes(Node origin, Node destination) {
        Node getRoad(Node position) { // ある点がそこから一番近い道にでるための関数、ある点が道上ならそのまま返す
            float leftSideRoad = position.X % 1;
            float downSideRoad = position.Y % 1;
            float rightSideRoad = 1f - leftSideRoad;
            float upSideRoad = 1f - downSideRoad;

            float minRoad = new float[] {leftSideRoad, downSideRoad, rightSideRoad, upSideRoad}.Min();

            if (minRoad == rightSideRoad) return new Node(Mathf.Ceil(position.X), position.Y);
            else if (minRoad == upSideRoad) return new Node(position.X, Mathf.Ceil(position.Y));     
            else if (minRoad == leftSideRoad) return new Node(Mathf.Floor(position.X), position.Y);     
            else return new Node(position.X, Mathf.Floor(position.Y));
        }

        Node getIntersection(Node position) { // ある点がそこから一番近い交差点に出るための関数
            return new Node(Mathf.Round(position.X), Mathf.Round(position.Y));
        }
        
        Node originToRoad = getRoad(origin), destinationToRoad = getRoad(destination); // origin, destination から道に出た時のそれぞれの座標
        
        // 碁盤の目状でランダムに進むための準備
        Node gobanFirst = getIntersection(originToRoad), gobanLast = getIntersection(destinationToRoad);
        int xMoving = (int)(gobanLast.X - gobanFirst.X), yMoving = (int)(gobanLast.Y - gobanFirst.Y); // gobanFirst から gobanLast まで x 方向、y 方向にいくら進むか。
        int n = Mathf.Abs(xMoving) + Mathf.Abs(yMoving) + 1; // 移動量
        
        var path = new List<Node>(); // 返り値
        path.Add(origin);
        if (origin != originToRoad) path.Add(originToRoad); // origin が道の上でないなら
        if (gobanFirst != origin) path.Add(gobanFirst); // origin が交差点上でないなら

        int plusOrMinusX = xMoving != 0 ? xMoving / Mathf.Abs(xMoving) : 1;
        int plusOrMinusY = yMoving != 0 ? yMoving / Mathf.Abs(yMoving) : 1; // gobanFirst から gobanLast までの各軸の移動の方向を表す
        
        // randomPath の生成
        while (xMoving != 0 || yMoving != 0) {
            var last = path.Last();
            if (xMoving != 0 && yMoving != 0) { // x 軸方向とy 軸方向共に移動可能か
                int rnd = Random.Range(0, 2); // 0 なら x 軸方向へ、1 なら y 軸方向へ
                if (rnd == 0) {
                    path.Add(new Node(last.X + plusOrMinusX, last.Y));
                    xMoving = xMoving - plusOrMinusX;
                } else {
                    path.Add(new Node(last.X, last.Y + plusOrMinusY));
                    yMoving = yMoving - plusOrMinusY;
                }
            } else {
                if (xMoving == 0) { // ずっと y 軸方向へ
                    path.Add(new Node(last.X, last.Y + plusOrMinusY));
                    yMoving = yMoving - plusOrMinusY;
                }
                else if (yMoving == 0) { // ずっと x 軸方向へ
                    path.Add(new Node(last.X + plusOrMinusX, last.Y));
                    xMoving = xMoving - plusOrMinusX;
                }
            }
        }

        if (gobanLast != destination) path.Add(destinationToRoad); // destination が交差点上でないなら
        if (destinationToRoad != destination) path.Add(destination); // destination が道の上でないなら
        
        var edges = new List<Edge>();
        for (int i = 0; i < path.Count - 1; i++) {
            Node from = path[i];
            Node to = path[i + 1];
            float cost = distance(from, to) * EdgeCost.Get(EdgeType.Walk);
            edges.Add(new Edge(from, to, cost, EdgeType.Walk));
        }
        return new MultiEdge(edges);
    }
}
