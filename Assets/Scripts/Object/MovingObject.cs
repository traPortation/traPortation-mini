using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Traffic.Node;
using Moving;

// 動くもの全般 (人間、乗り物など)
public abstract class MovingObject : MonoBehaviour
{
    protected Path path { get; private set; }

    protected float velocity;

    protected void Move(float delta)
    {
        var node = this.path.Move(delta);

        this.transform.position = new Vector3(this.path.X, this.path.Y, this.transform.position.z);

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
    /// Nodeに到着するごとに呼び出される
    /// </summary>
    /// <param name="node"></param>
    protected abstract void Arrive(INode node);
}
