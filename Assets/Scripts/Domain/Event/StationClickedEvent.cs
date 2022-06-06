using UnityEngine;

namespace TraPortation.Event
{
    public class StationClickedEvent
    {
        public readonly Vector2 Position;
        public StationClickedEvent(Vector2 position)
        {
            this.Position = position;
        }
    }
}