using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TraPortation.Event.Train;
using Zenject;

#nullable enable

namespace TraPortation.Moving.Section.Train
{
    public class TrainSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; private set; }
        int trainId;
        int index;
        int stopMiliSecond;
        bool direction;
        bool stopping;
        Station station => this.stations[this.index];
        Station nextStation => this.direction ? this.stations[this.index + 1] : this.stations[this.index - 1];
        IReadOnlyList<Station> stations;
        IPublisher<int, TrainEvent> trainPublisher;
        IPublisher<int, StationEvent> stationPublisher;

        public TrainSection(IReadOnlyList<Station> stations, int trainId, float stopSecond, IPublisher<int, TrainEvent> trainPublisher, IPublisher<int, StationEvent> stationPublisher)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(stations.First().Node);
            this.trainId = trainId;
            this.stopMiliSecond = (int)(stopSecond * 1000);
            this.index = 0;
            this.direction = true;
            this.stopping = false;
            this.stations = stations;
            this.trainPublisher = trainPublisher;
            this.stationPublisher = stationPublisher;

            if (this.stations.Count <= 1)
            {
                throw new ArgumentException();
            }
        }
        public class Factory : PlaceholderFactory<IReadOnlyList<Station>, int, float, TrainSection> { }

        public void Start()
        {
            this.Status = SectionStatus.TrainMoving;
        }

        public void Move(float distance)
        {
            if (this.Status != SectionStatus.TrainMoving || this.stopping) return;

            var nextStationPos = new Position(this.nextStation.Node);
            var goalDistance = Position.Distance(this.Position, nextStationPos);

            // 次の駅に着く場合
            if (goalDistance <= distance)
            {
                this.Position = nextStationPos;

                if (this.direction)
                {
                    this.index++;

                    if (this.index == this.stations.Count - 1)
                    {
                        this.direction = false;
                    }
                }
                else
                {
                    this.index--;

                    if (this.index == 0)
                    {
                        this.direction = true;
                    }
                }

                this.trainPublisher.Publish(this.trainId, new TrainEvent(this.station, this.nextStation));
                this.stationPublisher.Publish(this.station.ID, new StationEvent(this.trainId, this.nextStation));

                this.stopOnStation();
            }
            else
            {
                var ratio = distance / goalDistance;
                var x = this.Position.X + (nextStationPos.X - this.Position.X) * ratio;
                var y = this.Position.Y + (nextStationPos.Y - this.Position.Y) * ratio;

                this.Position = new Position(x, y);
            }
        }

        async void stopOnStation()
        {
            this.stopping = true;
            await UniTask.Delay(this.stopMiliSecond);
            this.stopping = false;
        }

        public void Dispose()
        {

        }
    }
}