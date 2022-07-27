namespace TraPortation.Event.Train
{
    /// <summary>
    /// 電車が駅に着いたときに駅ごとに発行されるイベント 
    /// </summary>
    public class StationEvent
    {
        public readonly int TrainId;
        public readonly Station NextStation;
        public StationEvent(int trainId, Station nextStation)
        {
            this.TrainId = trainId;
            this.NextStation = nextStation;
        }
    }
}