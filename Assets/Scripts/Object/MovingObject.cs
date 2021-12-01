using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 動くもの全般 (人間、乗り物など)
public abstract class MovingObject : MonoBehaviour
{
    protected Path path { get; private set; }

    protected float velocity;

    protected void Move(float delta)
    {
        var node = this.path.Move(delta);
        if (node != null)
        {
            this.Arrive(node);
        }
    }

    /// <summary>
    /// pathをsetし、自分の位置をpathの始点にする
    /// </summary>
    /// <param name="path"></param>
    public void Initialize(Path path)
    {
        this.path = path;
        this.transform.position = new Vector3(path.X, path.Y, transform.position.z);
    }

    /// <summary>
    /// 任意の始点と終点与えるといい感じのPathを与える
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    public void RandomInitialize(Vector3 start, Vector3 goal)
    {
        this.path = new Path(Board.Instance.GetPath(Board.Instance.GetNearestNode(start), Board.Instance.GetNearestNode(goal)), this.transform);
        var startNode = new BoardElements.PlotNode(start.x, start.y, 0);
        var goalNode = new BoardElements.PlotNode(goal.x, goal.y, this.path.Size()+1);
        this.path.AddPathNode(startNode, false);
        this.path.AddPathNode(goalNode);
        this.transform.position = new Vector3(path.X, path.Y, transform.position.z);
    }

    /// <summary>
    /// Nodeに到着するごとに呼び出される
    /// </summary>
    /// <param name="node"></param>
    protected abstract void Arrive(BoardElements.INode node);
}
