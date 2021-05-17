using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using Const;
using System.Linq;
using Collections;

public class Board
{
    private List<Vertex> graph;
    // コンストラクタ
    public Board() {
        graph = new List<Vertex>();
    }

    public void AddEdge(int from, int to, int cost, int type = EdgeType.Walk) {
        if (from >= this.size() || to >= this.size()) throw new System.Exception("vertex index is invalid");
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
    private void dijkstra(int start, int end, ref int[] dist, ref int[] from) {
        var que = new PriorityQueue<(int, int)>();
        
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
        const int INF = System.Int32.MaxValue;
        var dist = Enumerable.Repeat<int>(INF, this.size()).ToArray();
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
