using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TraPortation.Event.Train;
using TraPortation.Moving.Section;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation.Moving
{
    public class TrainPath : IPath
    {
        public Position Position => this.curSection.Position;
        public SectionStatus Status => this.curSection.Status;
        readonly IReadOnlyList<Station> stations;
        int index;
        int trainId;
        bool stopping;
        bool direction;
        // テストで書き換えるためにpublicにしている
        public int StopMilliseconds = 1000;
        ISection curSection;
        readonly IPublisher<int, TrainEvent> trainPub;
        readonly IPublisher<int, StationEvent> stationPub;

        public TrainPath(int trainId, IReadOnlyList<Station> stations, IPublisher<int, StationEvent> stationPub, IPublisher<int, TrainEvent> trainPub)
        {
            this.trainId = trainId;
            this.stations = stations;
            this.index = 1;
            this.stopping = false;
            this.direction = true;
            this.curSection = new SimpleSection(new List<Position> { new Position(stations[0].Node), new Position(stations[1].Node) });
            this.stationPub = stationPub;
            this.trainPub = trainPub;
        }

        public class Factory : PlaceholderFactory<int, IReadOnlyList<Station>, TrainPath> { }

        public void Move(float distance)
        {
            if (this.stopping) return;

            // Sectionが始まっていないなら始める
            if (this.curSection.Status == SectionStatus.NotStarted)
            {
                this.curSection.Start();
            }

            // Section内を移動する
            this.curSection.Move(distance);

            // Sectionが終わったら次のSectionに移る
            if (this.curSection.Status == SectionStatus.Finished)
            {
                this.curSection.Dispose();

                if (this.direction && this.index == this.stations.Count - 1)
                    this.direction = false;
                else if (!this.direction && this.index == 0)
                    this.direction = true;

                var station = this.stations[this.index];

                if (this.direction) this.index++;
                else this.index--;

                var nextStation = this.stations[this.index];
                this.stationPub.Publish(station.ID, new StationEvent(this.trainId, nextStation));
                this.trainPub.Publish(this.trainId, new TrainEvent(station, nextStation));

                // 止まる
                this.stopOnStation();

                // curSectionを更新
                this.curSection = new SimpleSection(new List<Position> { new Position(station.Node), new Position(nextStation.Node) });
            }
        }

        public void MoveTo(Vector3 vec)
        {
            var v = new Position(vec.x, vec.y);
            var mindist = float.MaxValue;
            var minindex = 0;
            Position a, b;
            for (int i = 0; i < this.stations.Count - 1; i++)
            {
                a = new Position(this.stations[i].Node);
                b = new Position(this.stations[i + 1].Node);

                var dist = v.DistanceToLine(a, b);
                if (mindist > dist)
                {
                    mindist = dist;
                    minindex = i;
                }
            }

            this.index = minindex + 1;
            a = new Position(this.stations[this.index - 1].Node);
            b = new Position(this.stations[this.index].Node);
            this.curSection = new SimpleSection(new List<Position> { a, b });

            var t = ((v.X - a.X) * (b.X - a.X) + (v.Y - a.Y) * (b.Y - a.Y)) / Position.Distance(a, b);
            this.curSection.Move(t);
        }

        async void stopOnStation()
        {
            this.stopping = true;
            await UniTask.Delay(this.StopMilliseconds);
            this.stopping = false;
        }

    }
}