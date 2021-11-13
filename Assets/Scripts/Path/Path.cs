using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

#nullable enable

public class Path
{
    IReadOnlyList<IIndexedEdge> edges;
    int index;
    public IIndexedNode LastNode => this.edges.Last().To;
    Transform transform;
    public float X {
        get => this.transform.position.x;
        private set {
            var pos = this.transform.position;
            pos.x = value;
            this.transform.position = pos;
        }
    }
    public float Y {
        get => this.transform.position.y;
        private set {
            var pos = this.transform.position;
            pos.y = value;
            this.transform.position = pos;
        }
    }

    /// <summary>
    /// 移動が終了しているかどうか
    /// </summary>
    public bool Finished => this.index >= this.edges.Count;
    public IIndexedNode? NextNode => !this.Finished ? this.edges[this.index].To : null;

    public Path(IReadOnlyList<IIndexedEdge> edges, Transform transform)
    {
        if (edges.Count == 0) throw new System.ArgumentException("edges are empty");

        this.edges = edges;
        this.X = edges[0].From.X;
        this.Y = edges[0].From.Y;
        this.index = 0;
        this.transform = transform;
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
        this.X = this.edges[0].From.X;
        this.Y = this.edges[0].From.Y;
    }
}
