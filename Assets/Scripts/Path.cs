using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

public class Path
{
    private List<Edge> edges;
    private int index;

    public float X, Y;

    /// <summary>
    /// 今いるEdgeのType
    /// 移動が完了している場合には -1
    /// </summary>
    /// <value></value>
    public Const.EdgeCost.Type Type => this.index < this.edges.Count ? this.edges[this.index].Type : Const.EdgeCost.Type.Finished;

    /// <summary>
    /// 移動が終了しているかどうか
    /// </summary>
    public bool Finished => index == this.edges.Count;
    public Node NextNode => edges[index].To;

    public Path(List<Edge> edges)
    {
        if (edges.Count == 0) throw new System.Exception("edges are empty");
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
    public Node Move(float delta)
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
    
    public Node Next()
    {
        if (this.Finished) return null;
        else
        {
            this.index++;
            if (this.Finished) return null;
            this.X = this.edges[this.index].From.X;
            this.Y = this.edges[this.index].From.Y;
            return this.edges[this.index].From;
        }
    }

    public void InitializeEdge()
    {
        this.index = 0;
        this.X = edges[0].From.X;
        this.Y = edges[0].From.Y;
    }
}
