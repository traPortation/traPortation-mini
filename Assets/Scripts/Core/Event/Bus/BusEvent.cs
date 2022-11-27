namespace TraPortation.Event.Bus
{
    /// <summary>
    /// バスが到着したときにバスごとに発行されるイベント
    /// </summary>
    public class BusEvent
    {
        public readonly BusStation BusStation;
        public readonly BusStation Next;
        public BusEvent(BusStation busStation, BusStation next)
        {
            this.BusStation = busStation;
            this.Next = next;
        }
    }
}