using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface ILine
    {
        void SetLine(Vector3[] positions);
        void SetColor(Color color);
    }
}
