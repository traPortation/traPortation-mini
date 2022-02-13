namespace Event.Train
{
    /// <summary>
    /// 電車が到着したときに電車ごとに発行されるイベント
    /// </summary>
	public class TrainEvent
	{
        public readonly Station Station;
        public readonly Station NextStation;
		public TrainEvent(Station station, Station nextStation)
		{
            this.Station = station;
            this.NextStation = nextStation;
        }
    }
}