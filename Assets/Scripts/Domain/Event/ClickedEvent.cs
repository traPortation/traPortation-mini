using UnityEngine;

namespace TraPortation.Event
{
    public class ClickedEvent
    {
        public readonly Vector2 Position;
        public ClickedEvent(Vector2 position)
        {
            this.Position = position;
        }
    }
}