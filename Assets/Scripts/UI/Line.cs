using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;

    // Startに書くと実行順序の問題でSetLineが先に実行されてしまうためここで初期化している
    [Inject]
    public void Construct() {
        this.lineRenderer = this.gameObject.AddComponent<LineRenderer>();
    }

    public void SetLine(Vector3[] positions) {
        this.lineRenderer.positionCount = positions.Count();
        this.lineRenderer.SetPositions(positions);

        // TODO: Constに置く / メソッドから変更可能にする
        this.lineRenderer.startWidth = 0.1f;
        this.lineRenderer.endWidth = 0.1f;
    }

    public void SetColor(Color color) {
        this.lineRenderer.startColor = color;
        this.lineRenderer.endColor = color;
    }
}
