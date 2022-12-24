using System.Collections;
using System.Collections.Generic;
using TraPortation.Traffic;
using Zenject;

#nullable enable

namespace TraPortation
{
    public class RailManager
    {
        List<Rail> rails { get; }
        public IReadOnlyList<Rail> Rails => this.rails;
        Rail.Factory railFactory;
        Board board;

        [Inject]
        public RailManager(Rail.Factory factory, Board board)
        {
            this.railFactory = factory;
            this.rails = new List<Rail>();
            this.board = board;
        }

        /// <summary>
        /// 路線を作成する
        /// </summary>
        public Rail AddRail(List<Station> stations)
        {
            int index = 0;
            if (this.rails.Count != 0)
            {
                index = this.rails[this.rails.Count - 1].ID + 1;
            }

            string defaultName = $"Rail {index.ToString()}";

            var rail = this.railFactory.Create(stations, index, defaultName);
            this.rails.Add(rail);

            for (int i = 0; i < stations.Count - 1; i++)
            {
                this.board.AddVehicleRoute(stations[i].Node, stations[i + 1].Node, EdgeType.Train);
                this.board.AddVehicleRoute(stations[i + 1].Node, stations[i].Node, EdgeType.Train);
            }

            return rail;
        }
    }
}