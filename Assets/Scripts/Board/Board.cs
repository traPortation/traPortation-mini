using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;
using Collections;

public class Board
{
    private List<Vertex> graph;
    // コンストラクタ
    public Board() {
        graph = new List<Vertex>();
    }

    private float distance(int from, int to) {
        return Mathf.Sqrt(Mathf.Pow(graph[from].X - graph[to].X, 2) + Mathf.Pow(graph[from].Y - graph[to].Y, 2));
    }

    public void AddEdge(int from, int to, int type = Const.Edge.Type.Walk) {
        if (from >= this.size() || to >= this.size()) throw new System.Exception("vertex index is invalid");
        float cost = Const.Edge.Cost(type) * distance(from, to);
        var edge = new Edge(from, to, cost, type);
        graph[from].Edges.Add(edge);
    }

    public void AddVertex(float x, float y) {
        var vertex = new Vertex(x, y);
        graph.Add(vertex);
    }

    private int size() {
        return this.graph.Count;
    }

    // start から end への最短経路を探す
    private void dijkstra(int start, int end, ref float[] dist, ref int[] from) {
        var que = new PriorityQueue<(float, int)>();
        
        que.Push((dist[start], start));
        while (que.Count != 0) {
            var (cost, idx) = que.Top;
            que.Pop();
            if (dist[end] < cost) break;
            if (dist[idx] < cost) continue;
            foreach (var edge in this.graph[idx].Edges) {
                var nextCost = cost + edge.Cost;
                if (dist[edge.To] <= nextCost) continue;
                dist[edge.To] = nextCost;
                from[edge.To] = idx;
                que.Push((dist[edge.To], edge.To));
            }
        }
    }
    public List<int> GetPath(int start, int end) {
        const float INF = float.MaxValue;
        var dist = Enumerable.Repeat<float>(INF, this.size()).ToArray();
        var from = Enumerable.Repeat<int>(-1, this.size()).ToArray();
        
        dijkstra(start, end, ref dist, ref from);
        var path = new List<int>();
        
        for (int cur = end; cur != -1; cur = from[cur]) {
            path.Add(cur);
        }
        path.Reverse();
        return path;
    }

}
