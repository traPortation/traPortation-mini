using UnityEngine;

namespace TraPortation.Event
{
    public class RoadClickedEvent
    {
        public readonly Vector2 Position;
        public RoadClickedEvent(Vector2 position)
        {
            this.Position = position;
        }
    }
}