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

        [Inject]
        public void ConstructRoad(GameManager manager)
        {
            this.transform.parent = manager.roadFolder.transform;
        }
    }
}