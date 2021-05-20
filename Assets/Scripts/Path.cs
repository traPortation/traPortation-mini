using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class Path 
{
    private List<Edge> edges;
    private int index;

    public float X, Y;

    public int Type { 
        get {
            if (index == edges.Count) return this.edges[this.index].Type;
            else return -1;
        }
    }
    public bool Finished {
        get {
            return index == edges.Count;
        }
    }
    public Path(List<Edge> edges) {
        if (edges.Count == 0) throw new System.Exception("edges are empty");
        this.edges = edges;
        this.index = 0;
        this.X = edges[0].From.X;
        this.Y = edges[0].From.Y;
    }
    public Vertex Move(float delta) {
        if (this.Finished) return null;
        var nextV = edges[index].To;
        float distance = Mathf.Sqrt(Mathf.Pow(nextV.X - this.X, 2) + Mathf.Pow(nextV.Y - this.Y, 2));
        if (distance > delta) {
            this.X += (nextV.X - this.X) * delta / distance;
            this.Y += (nextV.Y - this.Y) * delta / distance;
            return null;
        } else {
            this.X = nextV.X;
            this.Y = nextV.Y;
            this.index++;
            return nextV;
        }
    }
}
