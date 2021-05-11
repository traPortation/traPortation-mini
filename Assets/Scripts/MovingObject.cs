using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

// 動くもの全般 (人間、乗り物など)
public abstract class MovingObject : MonoBehaviour
{
    // Vector2にはそれぞれ通過点の座標が入る
    // それぞれの List<Vector2> の最後に到達するごとに、Arrive() が呼び出されるようになっている
    // Arrive() は継承先で必要に応じて実装する
    protected List<List<Vector2>> path;

    protected float velocity;

    // this.path[lastPoint.first][lastPoint.second] が最後に通った点になる
    protected (int first, int second) lastPoint = (0, 0);

    // delta だけ path 上を移動 主に Update() 内で呼び出して使用
    protected void Move(float delta) {
        (int f, int s) = this.lastPoint;

        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        Vector2 next = this.path[f][s + 1];
        float distance = Vector2.Distance(new Vector2(x, y), next);

        if (distance > delta) {
            // delta だけ移動
            transform.position = new Vector3((next.x - x) * (delta / distance) + x, (next.y - y) * (delta / distance) + y, z);
        } else {
            // 次のポイントまで移動
            transform.position = new Vector3(next.x, next.y, z);

            if (this.path[f].Count == s + 2) {
                // List<Vector2> の最後まで来たら Arrive() を呼び出して次の List に移動する
                this.lastPoint.first++;
                this.lastPoint.second = 0;
                this.Arrive(f);
            } else {
                this.lastPoint.second++;
            }
        }
    }

    // pathを渡すと初期化する 
    // Start() 内で呼び出したり path が変わったときに呼び出したり
    protected void Initialize(List<List<Vector2>> path) {
        this.path = path;
        this.lastPoint = (0, 0);
        transform.position = new Vector3(this.path[0][0].x, this.path[0][0].y, transform.position.z);
    }

    // それぞれの List<Vector2> の最後に到着したときに呼び出される
    // 継承先で実装する
    protected abstract void Arrive(int arriveIndex);
}
