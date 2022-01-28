using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

namespace UI
{

    /// <summary>
    /// UI上で直線を表示する
    /// </summary>
    public class Line : MonoBehaviour, ILine
    {
        LineRenderer lineRenderer;

        // Startに書くと実行順序の問題でSetLineが先に実行されてしまうためここで初期化している
        [Inject]
        void construct()
        {
            this.lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        }

        /// <summary>
        /// 線を引く
        /// </summary>
        public void SetLine(Vector3[] positions)
        {
            this.lineRenderer.positionCount = positions.Count();
            this.lineRenderer.SetPositions(positions);

            // TODO: Constに置く / メソッドから変更可能にする
            this.lineRenderer.startWidth = 0.1f;
            this.lineRenderer.endWidth = 0.1f;
        }

        /// <summary>
        /// 線の色を設定する
        /// </summary>
        public void SetColor(Color color)
        {
            this.lineRenderer.startColor = color;
            this.lineRenderer.endColor = color;
        }
    }
}