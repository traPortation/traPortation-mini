using TraPortation.Traffic;
using UnityEngine;

namespace TraPortation.UI
{
    public interface IRailView
    {
        Rail Rail { get; }
        void SetLine(Vector3[] positions);
        void SetColor(Color color);
        void SetRail(Rail rail);
    }
}