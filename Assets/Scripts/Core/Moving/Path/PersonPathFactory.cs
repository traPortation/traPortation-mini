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

            var walkSectionNodes = new List<INode>();
            var stations = new List<Station>();

            foreach (var node in nodes)
            {
                if (node is StationNode sNode && sNode.Kind == StationKind.Train)
                {
                    var station = this.stationManager.GetStation(sNode);
                    stations.Add(station);

                    if (walkSectionNodes.Count != 0)
                    {
                        // WalkSectionを追加
                        walkSectionNodes.Add(node);
                        var positions = walkSectionNodes.Select(n => new Position(n)).ToList();
                        sections.Add(new SimpleSection(positions));

                        walkSectionNodes = new List<INode>();
                    }
                }
                else
                {
                    walkSectionNodes.Add(node);

                    if (stations.Count > 1)
                    {
                        // TrainUsingSectionを追加
                        sections.Add(this.trainFactory.Create(stations));
                        stations = new List<Station>();
                    }
                }
            }

            // 最後のSectionを作成
            if (walkSectionNodes.Count > 1)
            {
                var positions = walkSectionNodes.Select(n => new Position(n)).ToList();
                sections.Add(new SimpleSection(positions));
            }
            else if (stations.Count > 1)
            {
                sections.Add(this.trainFactory.Create(stations));
            }

            return new PersonPath(sections);
        }
    }
}