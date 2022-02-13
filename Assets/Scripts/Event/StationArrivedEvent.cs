namespace Event
{
    /// <summary>
    /// 乗り物が駅に着いたときに発行されるイベント 
    /// </summary>
    public class StationArrivedEvent
    {
        public readonly int VehicleId;
        public readonly Station NextStation;
        public StationArrivedEvent(int vehicleId, Station nextStation)
        {
            this.VehicleId = vehicleId;
            this.NextStation = nextStation;
        }
    }
}