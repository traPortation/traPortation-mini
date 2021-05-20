using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;
using Collections;

public class Board
{
    private List<Vertex> vertexes;
    // コンストラクタ
    public Board() {
        vertexes = new List<Vertex>();
    }

    private float distance(Vertex from, Vertex to) {
        return Mathf.Sqrt(Mathf.Pow(from.X - to.X, 2) + Mathf.Pow(from.Y - to.Y, 2));
    }

    public void AddEdge(Vertex from, Vertex to, int type = Const.Edge.Type.Walk) {
        float cost = Const.Edge.Cost(type) * distance(from, to);
        var edge = new Edge(from, to, cost, type);
        from.Edges.Add(edge);
    }

    public Vertex AddVertex(float x, float y) {
        var vertex = new Vertex(x, y, this.size());
        vertexes.Add(vertex);
        return vertex;
    }

    private int size() {
        return this.vertexes.Count;
    }

    // start から goal への最短経路を探す
    private Edge[] dijkstra(Vertex start, Vertex goal) {
        const float INF = float.MaxValue;
        var dist = Enumerable.Repeat<float>(INF, this.size()).ToArray();
        var from = Enumerable.Repeat<Edge>(null, this.size()).ToArray();
        var que = new PriorityQueue<(float, int)>();
        
        dist[start.Index] = 0;
        que.Push((dist[start.Index], start.Index));
        while (que.Count != 0) {
            var (cost, idx) = que.Top;
            que.Pop();
            if (dist[idx] < cost) continue;
            foreach (var edge in this.vertexes[idx].Edges) {
                var nextCost = cost + edge.Cost;
                if (dist[edge.To.Index] <= nextCost) continue;
                dist[edge.To.Index] = nextCost;
                from[edge.To.Index] = edge;
                que.Push((dist[edge.To.Index], edge.To.Index));
            }
        }
        return from;
    }
    public Path GetPath(Vertex start, Vertex goal) {
        var from = dijkstra(start, goal);
        var edges = new List<Edge>();
        
        for (var cur = from[goal.Index]; cur != null; cur = from[cur.From.Index]) {
            edges.Add(cur);
        }
        edges.Reverse();
        return new Path(edges);
    }
    public Vertex GetRandomVertex() {
        return vertexes[Random.Range(0, this.size())];
    }
    public Path GetRandomPath(Vertex start) {
        while (true) {
            int idx = Random.Range(0, this.size());
            if (start != vertexes[idx]) return GetPath(start, vertexes[idx]);
        }
    }
}
