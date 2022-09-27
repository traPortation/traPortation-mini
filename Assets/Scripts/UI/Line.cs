using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace TraPortation.UI
{
    /// <summary>
    /// UI上で直線を表示する
    /// </summary>
    public class Line : MonoBehaviour, ILine
    {
        LineRenderer lineRenderer;
        BoxCollider2D boxCollider;

        // Startに書くと実行順序の問題でSetLineが先に実行されてしまうためここで初期化している
        [Inject]
        public void Construct()
        {
            this.lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            this.lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

            this.boxCollider = this.gameObject.AddComponent<BoxCollider2D>();
        }

        /// <summary>
        /// 線を引く
        /// </summary>
        public void SetLine(Vector3[] positions)
        {
            this.lineRenderer.positionCount = positions.Count();
            this.lineRenderer.SetPositions(positions);

            if (positions.Count() == 0)
            {
                return;
            }

            // TODO: Constに置く / メソッドから変更可能にする
            this.lineRenderer.startWidth = 0.1f;
            this.lineRenderer.endWidth = 0.1f;

            this.transform.position = new Vector3((positions.First().x + positions.Last().x) / 2, (positions.First().y + positions.Last().y) / 2, 2);

            var distance = Mathf.Sqrt(
                Mathf.Pow(positions.Last().x - positions.First().x, 2) +
                Mathf.Pow(positions.Last().y - positions.First().y, 2));
            this.boxCollider.size = new Vector2(distance, 0.3f);
            this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(positions.Last().y - positions.First().y, positions.Last().x - positions.First().x) * Mathf.Rad2Deg);
        }

        /// <summary>
        /// 線の色を設定する
        /// </summary>
        public void SetColor(Color color)
        {
            this.lineRenderer.startColor = color;
            this.lineRenderer.endColor = color;
        }

        public void SetZ(float z)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, z);
        }
    }
}