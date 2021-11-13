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

        var nextNode = this.edges[this.index].To;
        float distance = Mathf.Sqrt(Mathf.Pow(nextNode.X - this.X, 2) + Mathf.Pow(nextNode.Y - this.Y, 2));

        // 次のNodeに着く場合
        if (distance <= delta)
        {
            this.X = nextNode.X;
            this.Y = nextNode.Y;
            this.index++;
            return nextNode;
        }
        // 届かない場合
        else
        {
            this.X += (nextNode.X - this.X) * delta / distance;
            this.Y += (nextNode.Y - this.Y) * delta / distance;
            return null;
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
