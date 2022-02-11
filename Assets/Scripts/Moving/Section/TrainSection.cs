using System.Collections.Generic;
using System.Linq;
using Zenject;
using MessagePipe;
using Event;

#nullable enable

namespace Moving.Section
{
    public class TrainSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; }
        readonly IReadOnlyList<Station> stations;
        int index;
        readonly ISubscriber<int, StationArrivedEvent> stationSubscriber;
        readonly ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber;
        readonly DisposableBagBuilder disposableBag;

        [Inject]
        TrainSection(IReadOnlyList<Station> stations, ISubscriber<int, StationArrivedEvent> stationSubscriber, ISubscriber<int, VehicleArrivedEvent> vehicleSubscriber)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(stations.First().Node);
            this.stations = stations;
            this.stationSubscriber = stationSubscriber;
            this.vehicleSubscriber = vehicleSubscriber;
            this.disposableBag = DisposableBag.CreateBuilder();
        }

        public class Factory : PlaceholderFactory<IReadOnlyList<Station>, TrainSection> { }

        public void Start()
        {
            this.Status = SectionStatus.OnStation;
            this.index = 0;

            if (this.stations.Count <= 1)
            {
                this.Status = SectionStatus.Finished;
                return;
            }

            this.waitOnStation();
        }
        public void Move(float pos)
        {
        }

        void waitOnStation()
        {
            this.Dispose();

            this.Status = SectionStatus.OnStation;
            var stationId = this.stations[this.index].ID;
            if (this.index >= this.stations.Count - 1)
            {
                this.Status = SectionStatus.Finished;
                return;
            }

            this.stationSubscriber.Subscribe(stationId, se =>
            {
                if (this.Status != SectionStatus.OnStation || se.NextStation != this.stations[this.index + 1]) return;

                // TODO: 乗れない場合を考慮する

                this.Status = SectionStatus.OnTrain;

                this.vehicleSubscriber.Subscribe(se.Vehicle.ID, ve =>
                {
                    this.index++;

                    // 終わりの場合
                    if (this.index >= this.stations.Count - 1)
                    {
                        this.Status = SectionStatus.Finished;
                    }
                    else if (this.stations[this.index + 1] != ve.NextStation)
                    {
                        this.waitOnStation();
                    }
                    else
                    {

                    }


                }).AddTo(this.disposableBag);

            }).AddTo(this.disposableBag);
        }

        public void Dispose()
        {
            var disposable = this.disposableBag.Build();
            disposable.Dispose();
        }
    }
}