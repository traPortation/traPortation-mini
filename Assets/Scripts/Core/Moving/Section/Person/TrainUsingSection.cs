using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Event.Train;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation.Moving.Section.Person
{
    public class TrainUsingSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; }
        public Quaternion Rotation { get; } = Quaternion.identity;
        readonly IReadOnlyList<Station> stations;
        int index;
        readonly ISubscriber<int, StationEvent> stationSubscriber;
        readonly ISubscriber<int, TrainEvent> trainSubscriber;
        readonly DisposableBagBuilder disposableBag;
        readonly ManageMoney manager;

        public TrainUsingSection(IReadOnlyList<Station> stations, ISubscriber<int, StationEvent> stationSubscriber, ISubscriber<int, TrainEvent> trainSubscriber, ManageMoney manager)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(stations.First().Node);
            this.stations = stations;
            this.stationSubscriber = stationSubscriber;
            this.trainSubscriber = trainSubscriber;
            this.disposableBag = DisposableBag.CreateBuilder();
            this.manager = manager;

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

            this.waitOnStation(this.stations[0]);
        }
        public void Move(float distance)
        {
        }

        void waitOnStation(Station station)
        {
            // 今あるイベント購読を解除 (再帰的に呼ばれることがあるため)
            this.Dispose();

            this.Status = SectionStatus.OnStation;

            // 駅で電車を待つ
            this.stationSubscriber.Subscribe(station.ID, se =>
            {
                // 来た電車について次の駅が同じかを確認
                if (this.Status != SectionStatus.OnStation || se.NextStation != this.stations[this.index + 1]) return;

                // TODO: 乗れない場合を考慮する

                this.Status = SectionStatus.OnTrain;

                // 運賃を加算して電車に乗る
                this.manager.ExpenseMoney(Const.Train.Wage);
                this.trainSubscriber.Subscribe(se.TrainId, ve =>
                {
                    // 自分が乗った駅への到着は無視する
                    if (ve.Station == station) return;

                    this.index++;

                    Debug.Assert(this.stations[this.index] == ve.Station);

                    // 移動が終わりの場合
                    if (this.index >= this.stations.Count - 1)
                    {
                        this.Status = SectionStatus.Finished;
                    }
                    // 移動は続くが電車からは降りる場合
                    else if (this.stations[this.index + 1] != ve.NextStation)
                    {
                        this.waitOnStation(this.stations[this.index]);
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