using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace TraPortation.Traffic
{
    public class BusRail
    {
        List<Bus> buses;
        IReadOnlyList<BusRoute> routes;
        IBusRailView line;
        public BusRail(IReadOnlyList<IBoardNode> nodes, IBusRailView line, BusStationManager manager)
        {
            Assert.IsNotNull(nodes.First() as StationNode);
            var start = nodes.First() as StationNode;
            Assert.AreEqual(start.Kind, StationKind.Bus);

            var positions = new List<Position>() { new Position(start) };
            var routes = new List<BusRoute>();

            for (int i = 1; i < nodes.Count; i++)
            {
                positions.Add(new Position(nodes[i]));

                if (nodes[i] is StationNode end && end.Kind == StationKind.Bus)
                {
                    var s = manager.GetBusStation(start);
                    var e = manager.GetBusStation(end);
                    var route = new BusRoute(s, e, positions);
                    routes.Add(route);
                    start = end;
                    positions = new List<Position>() { new Position(start) };
                }
            }

            this.routes = routes;
            this.line = line;

            this.line.SetLine(nodes.Select(n => new Vector3(n.X, n.Y, 8.0f)).ToArray());
            this.line.SetColor(Color.yellow);
        }

        public class Factory : PlaceholderFactory<IReadOnlyList<IBoardNode>, BusRail> { }

        public void AddBus(Bus bus)
        {
            this.buses.Add(bus);

            var sections = new List<ISection>();
        }
    }
}