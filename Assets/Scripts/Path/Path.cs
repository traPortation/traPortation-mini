using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

#nullable enable

public class Path
{
    private List<IIndexedEdge> edges;
    private int index;
    public IIndexedNode LastNode => edges.Last().To;

    public float X { get; private set; }
    public float Y { get; private set; }

    /// <summary>
    /// 移動が終了しているかどうか
    /// </summary>
    public bool Finished => index >= this.edges.Count;
    public IIndexedNode? NextNode => !this.Finished ? this.edges[this.index].To : null;

    public Path(List<IIndexedEdge> edges)
    {
        if (edges.Count == 0) throw new System.ArgumentException("edges are empty");
        this.edges = edges;
        this.index = 0;
        this.X = edges[0].From.X;
        this.Y = edges[0].From.Y;
    }
    /// <summary>
    /// deltaだけpath上を移動する
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>Nodeに到達した場合はそのNode、していない場合はnull</returns>
    public IIndexedNode? Move(float delta)
    {
        if (this.Finished) return null;
        var nextV = this.edges[this.index].To;
        float distance = Mathf.Sqrt(Mathf.Pow(nextV.X - this.X, 2) + Mathf.Pow(nextV.Y - this.Y, 2));
        if (distance > delta)
        {
            this.X += (nextV.X - this.X) * delta / distance;
            this.Y += (nextV.Y - this.Y) * delta / distance;
            return null;
        }
        else
        {
            this.X = nextV.X;
            this.Y = nextV.Y;
            this.index++;
            return nextV;
        }
    }

    public IIndexedNode? MoveNext()
    {
        if (this.Finished) return null;

        var node = this.NextNode;
        if (node != null)
        {
            this.X = node.X;
            this.Y = node.Y;
            this.index++;
        }

        return node;
    }

    public void InitializeEdge()
    {
        this.index = 0;
        this.X = edges[0].From.X;
        this.Y = edges[0].From.Y;
    }
}
