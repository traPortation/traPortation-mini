using System.Collections;
using System.Collections.Generic;
using TraPortation.Traffic;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation
{
    public class RailManager
    {
        List<Rail> rails { get; }
        public IReadOnlyList<Rail> Rails => this.rails;
        Rail.Factory railFactory;
        public Color NextColor => Const.Color.RailColors[this.rails.Count % Const.Color.RailColors.Count];
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
            int index = this.rails.Count;

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