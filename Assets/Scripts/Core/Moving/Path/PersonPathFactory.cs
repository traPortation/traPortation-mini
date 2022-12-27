using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving.Section;
using TraPortation.Moving.Section.Person;
using TraPortation.Traffic;
using TraPortation.Traffic.Node;
using UnityEngine;
using Zenject;

#nullable enable

namespace TraPortation.Moving
{
    public class PersonPathFactory
    {
        readonly TrainUsingSection.Factory trainFactory;
        readonly BusUsingSection.Factory busFactory;
        readonly StationManager stationManager;
        readonly BusStationManager busStationManager;

        [Inject]
        public PersonPathFactory(TrainUsingSection.Factory trainFactory, BusUsingSection.Factory busFactory, StationManager stationManager, BusStationManager busStationManager)
        {
            this.trainFactory = trainFactory;
            this.busFactory = busFactory;
            this.stationManager = stationManager;
            this.busStationManager = busStationManager;
        }

        public PersonPath Create(IReadOnlyList<INode> nodes)
        {
            var sections = new List<ISection>();

            var positions = new List<Position>();

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (node is StationNode sNode)
                {
                    positions.Add(new Position(node));
                    sections.Add(new SimpleSection(positions));
                    positions = new List<Position>();

                    if (sNode.Kind == StationKind.Train)
                    {
                        var stations = new List<Station>();
                        while (i < nodes.Count && nodes[i] is StationNode s && sNode.Kind == StationKind.Train)
                        {
                            stations.Add(this.stationManager.GetStation(s));
                            i++;
                        }
                        i--;

                        if (stations.Count > 1)
                            sections.Add(this.trainFactory.Create(stations));

                        positions.Add(new Position(nodes[i]));
                    }

                    else
                    {
                        var busStations = new List<BusStation>();
                        while (i < nodes.Count && nodes[i] is StationNode s && sNode.Kind == StationKind.Bus)
                        {
                            busStations.Add(this.busStationManager.GetBusStation(s));
                            i++;
                        }
                        i--;

                        if (busStations.Count > 1)
                            sections.Add(this.busFactory.Create(busStations));

                        positions.Add(new Position(nodes[i]));
                    }


                }
                else
                {
                    positions.Add(new Position(node));
                }
            }
            if (positions.Count > 1)
            {
                sections.Add(new SimpleSection(positions));
            }

            return new PersonPath(sections);
        }
    }
}