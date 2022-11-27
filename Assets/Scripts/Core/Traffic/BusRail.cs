using System.Collections.Generic;
using TraPortation.Moving;

namespace TraPortation.Traffic
{
    public class BusRail
    {
        List<Bus> buses;
        IReadOnlyList<BusRoute> routes;
        public BusRail()
        {
            // TODO: 初期化
        }

        public void AddBus(Bus bus)
        {
            this.buses.Add(bus);

            var sections = new List<ISection>();
        }
    }
}