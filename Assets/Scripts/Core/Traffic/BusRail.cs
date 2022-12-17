using System.Collections.Generic;
using System.Linq;
using TraPortation.Const;
using TraPortation.Moving;
using TraPortation.Traffic.Node;
using TraPortation.UI;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

#nullable enable

namespace TraPortation.Traffic
{
    public class BusRail
    {
        List<Bus> buses;
        IReadOnlyList<BusRoute> routes;
        IBusRailView line;
        BusPath.Factory factory;
        public BusRail(IReadOnlyList<IBoardNode> nodes, IBusRailView line, BusStationManager manager, BusPath.Factory factory)
        {
            var start = nodes.First() as StationNode;
            if (start == null)
            {
                throw new System.Exception("First node is not StationNode");
            }
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
            this.factory = factory;
            this.buses = new List<Bus>();

            this.line.SetLine(nodes.Select(n => new Vector3(n.X, n.Y, Z.BusRail)).ToArray());
            this.line.SetColor(Color.yellow);
            this.line.SetRail(this);
        }

        public class Factory : PlaceholderFactory<IReadOnlyList<IBoardNode>, BusRail> { }

        public void AddBus(Bus bus, Vector3 vec)
        {
            this.buses.Add(bus);

            var path = this.factory.Create(bus.ID, this.routes);
            path.MoveTo(vec);
            bus.Initialize(path);
        }
    }
}