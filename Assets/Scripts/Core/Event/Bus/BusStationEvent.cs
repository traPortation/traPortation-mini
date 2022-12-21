namespace TraPortation.Event.Bus
{
    /// <summary>
    /// バスが到着したときにバス停ごとに発行されるイベント
    /// </summary>
    public class BusStationEvent
    {
        public readonly int BusId;
        public readonly BusStation Next;
        public BusStationEvent(int busId, BusStation next)
        {
            this.BusId = busId;
            this.Next = next;
        }
    }
}