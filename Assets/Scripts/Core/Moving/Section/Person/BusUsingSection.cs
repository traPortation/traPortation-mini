using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using TraPortation.Event.Bus;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation.Moving.Section.Person
{
    public class BusUsingSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; }
        public Quaternion Rotation { get; } = Quaternion.identity;
        readonly IReadOnlyList<BusStation> busStations;
        int index;
        readonly ISubscriber<int, BusStationEvent> busStationSubscriber;
        readonly ISubscriber<int, BusEvent> busSubscriber;
        readonly DisposableBagBuilder disposableBag;
        readonly ManageMoney manager;

        public BusUsingSection(IReadOnlyList<BusStation> busStations, ISubscriber<int, BusStationEvent> busStationSubscriber, ISubscriber<int, BusEvent> busSubscriber, ManageMoney manager)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(busStations.First().Node);
            this.busStations = busStations;
            this.busStationSubscriber = busStationSubscriber;
            this.busSubscriber = busSubscriber;
            this.disposableBag = DisposableBag.CreateBuilder();
            this.manager = manager;

            if (this.busStations.Count <= 1)
            {
                throw new System.ArgumentException();
            }
        }

        public class Factory : PlaceholderFactory<IReadOnlyList<BusStation>, BusUsingSection> { }

        public void Start()
        {
            this.Status = SectionStatus.OnBusStation;
            this.index = 0;

            this.waitOnBusStation(this.busStations[0]);
        }

        public void Move(float distance)
        {
        }

        void waitOnBusStation(BusStation busStation)
        {
            // 今あるイベント購読を解除 (再帰的に呼ばれることがあるため)
            this.Dispose();

            this.Status = SectionStatus.OnBusStation;

            // 駅で電車を待つ
            this.busStationSubscriber.Subscribe(busStation.ID, se =>
            {
                // 来た電車について次の駅が同じかを確認
                if (this.Status != SectionStatus.OnBusStation || se.Next != this.busStations[this.index + 1]) return;

                // TODO: 乗れない場合を考慮する

                this.Status = SectionStatus.OnBus;

                // 運賃を加算して電車に乗る
                this.manager.ExpenseMoney(Const.Bus.Wage);
                this.busSubscriber.Subscribe(se.BusId, ve =>
                {
                    // 自分が乗った駅への到着は無視する
                    if (ve.BusStation == busStation) return;

                    this.index++;

                    Debug.Assert(this.busStations[this.index] == ve.BusStation);

                    // 移動が終わりの場合
                    if (this.index >= this.busStations.Count - 1)
                    {
                        this.Status = SectionStatus.Finished;
                    }
                    // 移動は続くが電車からは降りる場合
                    else if (this.busStations[this.index + 1] != ve.Next)
                    {
                        this.waitOnBusStation(this.busStations[this.index]);
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