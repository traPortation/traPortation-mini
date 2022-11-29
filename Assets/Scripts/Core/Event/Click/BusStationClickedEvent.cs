using UnityEngine;

namespace TraPortation.Event
{
    public class BusStationClickedEvent
    {
        public readonly Vector2 Position;
        public readonly BusStation BusStation;
        public BusStationClickedEvent(Vector2 position, BusStation busStation)
        {
            this.Position = position;
            this.BusStation = busStation;
        }
    }
}