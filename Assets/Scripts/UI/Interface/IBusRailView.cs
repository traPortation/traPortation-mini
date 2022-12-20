using TraPortation.Traffic;
using UnityEngine;

namespace TraPortation.UI
{
    public interface IBusRailView
    {
        void SetLine(Vector3[] positions);
        void SetColor(Color color);
        void SetRail(BusRail rail);
    }
}