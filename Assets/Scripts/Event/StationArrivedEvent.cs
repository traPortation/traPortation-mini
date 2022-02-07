namespace Event
{
    /// <summary>
    /// 乗り物が駅に着いたときに発行されるイベント 
    /// </summary>
    public class StationArrivedEvent
    {
        public readonly Vehicle Vehicle;
        public readonly Station NextStation;
        public StationArrivedEvent(Vehicle vehicle, Station nextStation)
        {
            this.Vehicle = vehicle;
            this.NextStation = nextStation;
        }
    }
}