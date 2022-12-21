using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TraPortation.Event.Bus;
using TraPortation.Moving.Section;
using TraPortation.Traffic;
using UnityEngine;
using Zenject;

namespace TraPortation.Moving
{
    public class BusPath : IPath
    {
        public Position Position => this.curSection.Position;
        public Quaternion Rotation => this.curSection.Rotation;
        public SectionStatus Status => this.curSection.Status;
        public int StopMilliseconds = Const.Bus.StopMilliseconds;
        ISection curSection;
        int busId;
        int index;
        bool stopping;
        bool direction;
        IReadOnlyList<BusRoute> routes;
        IPublisher<int, BusEvent> busPub;
        IPublisher<int, BusStationEvent> busStationPub;

        public BusPath(int busId, IReadOnlyList<BusRoute> routes, IPublisher<int, BusEvent> busPub, IPublisher<int, BusStationEvent> busStationPub)
        {
            this.busId = busId;
            this.routes = routes;
            this.curSection = new SimpleSection(routes[0].Positions);
            this.busPub = busPub;
            this.busStationPub = busStationPub;
        }

        public class Factory : PlaceholderFactory<int, IReadOnlyList<BusRoute>, BusPath> { }

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

                if (this.direction && this.index == this.routes.Count - 1)
                    this.direction = false;
                else if (!this.direction && this.index == 0)
                    this.direction = true;

                if (this.direction) this.index++;
                else this.index--;

                var nextRoute = this.routes[this.index];
                this.busStationPub.Publish(nextRoute.Start.ID, new BusStationEvent(this.busId, nextRoute.End));
                this.busPub.Publish(this.busId, new BusEvent(nextRoute.Start, nextRoute.End));

                // 止まる
                this.stopOnStation();

                // curSectionを更新
                this.curSection = new SimpleSection(nextRoute.Positions);
            }
        }

        public void MoveTo(Vector3 vec)
        {
            var v = new Position(vec.x, vec.y);
            var mindist = float.MaxValue;
            BusRoute minRoute = null;
            int minindex = 0;
            Position a, b;
            foreach (var route in this.routes)
            {
                for (var i = 0; i < route.Positions.Count - 1; i++)
                {
                    a = route.Positions[i];
                    b = route.Positions[i + 1];
                    var dist = v.DistanceToLine(a, b);
                    if (dist < mindist)
                    {
                        mindist = dist;
                        minRoute = route;
                        minindex = i;
                    }
                }
            }

            this.curSection = new SimpleSection(minRoute.Positions);
            for (var i = 0; i < minindex; i++)
            {
                this.curSection.Move(float.MaxValue);
            }
            this.curSection.Move(Position.Distance(minRoute.Positions[minindex], v));
        }

        async void stopOnStation()
        {
            this.stopping = true;
            await UniTask.Delay(this.StopMilliseconds);
            this.stopping = false;
        }
    }
}