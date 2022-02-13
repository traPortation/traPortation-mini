using System.Collections.Generic;
using System.Linq;
using Zenject;
using MessagePipe;
using Event.Train;

#nullable enable

namespace Moving.Section.Person
{
    public class TrainUsingSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; }
        readonly IReadOnlyList<Station> stations;
        int index;
        readonly ISubscriber<int, StationEvent> stationSubscriber;
        readonly ISubscriber<int, TrainEvent> trainSubscriber;
        readonly DisposableBagBuilder disposableBag;

        public TrainUsingSection(IReadOnlyList<Station> stations, ISubscriber<int, StationEvent> stationSubscriber, ISubscriber<int, TrainEvent> trainSubscriber)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(stations.First().Node);
            this.stations = stations;
            this.stationSubscriber = stationSubscriber;
            this.trainSubscriber = trainSubscriber;
            this.disposableBag = DisposableBag.CreateBuilder();

            if (this.stations.Count <= 1)
            {
                throw new System.ArgumentException();
            }
        }

        public class Factory : PlaceholderFactory<IReadOnlyList<Station>, TrainUsingSection> { }

        public void Start()
        {
            this.Status = SectionStatus.OnStation;
            this.index = 0;

            this.waitOnStation();
        }
        public void Move(float pos)
        {
        }

        void waitOnStation()
        {
            // 今あるイベント購読を解除 (再帰的に呼ばれることがあるため)
            this.Dispose();

            this.Status = SectionStatus.OnStation;
            var stationId = this.stations[this.index].ID;

            // 駅で電車を待つ
            this.stationSubscriber.Subscribe(stationId, se =>
            {
                // 来た電車について次の駅が同じかを確認
                if (this.Status != SectionStatus.OnStation || se.NextStation != this.stations[this.index + 1]) return;

                // TODO: 乗れない場合を考慮する

                this.Status = SectionStatus.OnTrain;

                // 電車に乗る
                this.trainSubscriber.Subscribe(se.TrainId, ve =>
                {
                    this.index++;

                    // 移動が終わりの場合
                    if (this.index >= this.stations.Count - 1)
                    {
                        this.Status = SectionStatus.Finished;
                    }
                    // 移動は続くが電車からは降りる場合
                    else if (this.stations[this.index + 1] != ve.NextStation)
                    {
                        this.waitOnStation();
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