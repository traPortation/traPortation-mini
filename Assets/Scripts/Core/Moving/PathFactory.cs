using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving.Section.Person;
using TraPortation.Traffic.Node;
using Zenject;

#nullable enable

namespace TraPortation.Moving
{
    public class PathFactory
    {
        readonly TrainUsingSection.Factory trainFactory;
        readonly StationManager stationManager;

        [Inject]
        public PathFactory(TrainUsingSection.Factory trainFactory, StationManager stationManager)
        {
            this.trainFactory = trainFactory;
            this.stationManager = stationManager;
        }

        public Path Create(IReadOnlyList<INode> nodes)
        {
            var sections = new List<ISection>();

            var walkSectionNodes = new List<INode>();
            var stations = new List<Station>();

            foreach (var node in nodes)
            {
                if (node is StationNode sNode)
                {
                    var station = this.stationManager.GetStation(sNode);
                    stations.Add(station);

                    if (walkSectionNodes.Count != 0)
                    {
                        // WalkSectionを追加
                        walkSectionNodes.Add(node);
                        var positions = walkSectionNodes.Select(n => new Position(n)).ToList();
                        sections.Add(new WalkSection(positions));

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
                sections.Add(new WalkSection(positions));
            }
            else if (stations.Count > 1)
            {
                sections.Add(this.trainFactory.Create(stations));
            }

            return new Path(sections, nodes.Last());
        }
    }
}