using System.Collections;
using System.Collections.Generic;
using TraPortation.Moving;
using UnityEngine;

// 動くもの全般 (人間、乗り物など)
public abstract class MovingObject : MonoBehaviour
{
    protected Path path { get; private set; }
    protected float velocity;

    protected void Move(float delta)
    {
        this.path.Move(delta);
        this.transform.position = this.path.Position.ToVector3(transform.position.z);
    }

    /// <summary>
    /// pathをsetし、自分の位置をpathの始点にする
    /// </summary>
    /// <param name="path"></param>
    public void Initialize(Path path)
    {
        this.path = path;
        this.transform.position = this.path.Position.ToVector3(transform.position.z);
    }
}
