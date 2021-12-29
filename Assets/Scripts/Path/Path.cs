using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

#nullable enable

public class Path
{
    List<PathNode> nodes;
    int index;
    public INode LastNode => this.nodes.Last().Node;
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
    public bool Finished => this.index >= this.nodes.Count - 1;
    public INode? NextNode => !this.Finished ? this.nodes[this.index + 1].Node : null;

    public Path(List<PathNode> nodes, Transform transform)
    {
        if (nodes.Count == 0) throw new System.ArgumentException("nodes are empty");

        this.nodes = nodes;
        this.transform = transform;
        this.X = nodes[0].Node.X;
        this.Y = nodes[0].Node.Y;
        this.index = 0;
    }

    /// <summary>
    /// PathにPathNodeを前後どちらかにつける。
    /// </summary>
    /// <param name="node"></param>
    /// <param name="isAddTail"></param>
    /// <returns>void</returns>
    public void AddPathNode(IIndexedNode node, bool isAddTail=true)
    {
        if (this.nodes.Count <= 0) throw new System.ArgumentException("nodes are empty");

        // Path末端にnodeを置くとき
        if (isAddTail)
        {
            this.nodes.Add(new PathNode(node, null));
        }

        // Path先頭にnodeを置くとき
        else 
        {
            this.nodes.Insert(0, new PathNode(node, new PlotEdge<INode>(this.nodes[0].Node, 0)));
            this.InitializeEdge();
        }
    }

    /// <summary>
    /// deltaだけpath上を移動する
    /// </summary>
    /// <param name="delta"></param>
    /// <returns>Nodeに到達した場合はそのNode、していない場合はnull</returns>
    public INode? Move(float delta)
    {
        var nextNode = this.NextNode;
        if (nextNode == null) return null;

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

    public INode? MoveNext()
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
        this.X = this.nodes[0].Node.X;
        this.Y = this.nodes[0].Node.Y;
    }
    public int Size()
    {
        return this.nodes.Count;
    }
}
