using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Line : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Start()
    {
        this.lineRenderer = this.gameObject.AddComponent<LineRenderer>();
    }

    void SetLine(Vector3[] positions) {
        this.lineRenderer.SetPositions(positions);
    }

    void SetColor(Color color) {
        this.lineRenderer.startColor = color;
        this.lineRenderer.endColor = color;
    }
}
