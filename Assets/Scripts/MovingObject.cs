using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    protected List<List<Vector2>> path;

    protected float velocity;

    // this.path[lastPoint.first][lastPoint.second] が最後に通った点になる
    protected (int first, int second) lastPoint = (0, 0);

    // delta だけ移動 主に Update() 内で呼び出して使用
    protected void Move(float delta) {
        Vector2 nextPoint = this.path[this.lastPoint.first][this.lastPoint.second + 1];
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), nextPoint);
        if (distance > delta) {
            transform.position = new Vector3((nextPoint.x - transform.position.x) * (delta / distance) + transform.position.x, (nextPoint.y - transform.position.y) * (delta / distance) + transform.position.y, transform.position.z);
        } else {
            transform.position = this.path[this.lastPoint.first][this.lastPoint.second + 1];
            if (this.path[this.lastPoint.first].Count == this.lastPoint.second + 2) {
                this.lastPoint.first += 1;
                this.lastPoint.second = 0;
                this.Arrive(this.lastPoint.first - 1);
            } else {
                this.lastPoint.second += 1;
            }
        }
    }

    // pathを渡すと初期化する Start() 内で呼び出したり path が変わったときに呼び出したり
    protected void Initialize(List<List<Vector2>> path) {
        this.path = path;
        this.lastPoint.first = 0;
        this.lastPoint.second = 0;
        transform.position = new Vector3(this.path[0][0].x, this.path[0][0].y, 0f);
    }

    // それぞれの List<Vector2> の最後に到着したときに行う処理
    protected abstract void Arrive(int arriveIndex);
}
