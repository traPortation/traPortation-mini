using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MessagePipe;
using TraPortation.Event.Bus;
using TraPortation.Moving.Section;
using TraPortation.Traffic;
using Zenject;

namespace TraPortation.Moving
{
    public class BusPath : IPath
    {
        public Position Position => this.curSection.Position;
        public SectionStatus Status => this.curSection.Status;
        public int StopMilliseconds = 1000;
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

        async void stopOnStation()
        {
            this.stopping = true;
            await UniTask.Delay(this.StopMilliseconds);
            this.stopping = false;
        }
    }
}