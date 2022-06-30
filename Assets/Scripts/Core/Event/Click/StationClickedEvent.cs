using UnityEngine;

namespace TraPortation.Event
{
	public class StationClickedEvent
	{
		public readonly Vector2 Position;
		public readonly Station Station;
		public StationClickedEvent(Vector2 position, Station station)
		{
			this.Position = position;
			this.Station = station;
		}
	}
}