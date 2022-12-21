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
        readonly StationManager stationManager;

        [Inject]
        public PersonPathFactory(TrainUsingSection.Factory trainFactory, StationManager stationManager)
        {
            this.trainFactory = trainFactory;
            this.stationManager = stationManager;
        }

        public PersonPath Create(IReadOnlyList<INode> nodes)
        {
            var sections = new List<ISection>();

            var positions = new List<Position>();

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                if (node is StationNode sNode && sNode.Kind == StationKind.Train && nodes[i + 1] is StationNode sNext && sNext.Kind == StationKind.Train)
                {
                    positions.Add(new Position(node));
                    sections.Add(new SimpleSection(positions));
                    positions = new List<Position>();

                    var stations = new List<Station>();
                    while (i < nodes.Count && nodes[i] is StationNode s && sNode.Kind == StationKind.Train)
                    {
                        stations.Add(this.stationManager.GetStation(s));
                        i++;
                    }
                    i--;
                    positions.Add(new Position(nodes[i]));
                    sections.Add(this.trainFactory.Create(stations));
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