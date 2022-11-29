using MessagePipe;
using TraPortation.Event;
using TraPortation.Traffic.Edge;
using UnityEngine.EventSystems;
using Zenject;

namespace TraPortation.UI
{
    public class RoadView : Line, IRoadView
    {
        public RoadEdge edge { get; }
    }
}