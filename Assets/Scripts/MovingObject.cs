using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

// 動くもの全般 (人間、乗り物など)
public abstract class MovingObject : MonoBehaviour
{
    protected Path path;

    protected float velocity;

    protected void Move(float delta) {
        var node = path.Move(delta);
        transform.position = new Vector3(path.X, path.Y, transform.position.z);
        if (node != null) Arrive(node);
    }
    // Start() 内で呼び出したり path が変わったときに呼び出したり
    protected void Initialize(Path path) {
        this.path = path;
        transform.position = new Vector3(path.X, path.Y, transform.position.z);
    }

    // 継承先で実装する
    protected abstract void Arrive(BoardElements.Node vertex);
}
