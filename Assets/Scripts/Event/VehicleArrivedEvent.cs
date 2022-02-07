namespace Event
{
	public class VehicleArrivedEvent
	{
        public readonly Station Station;
        public readonly Station NextStation;
		public VehicleArrivedEvent(Station station, Station nextStation)
		{
            this.Station = station;
            this.NextStation = nextStation;
        }
    }
}