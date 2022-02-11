using System.Collections.Generic;
using System.Linq;
using Zenject;
using Traffic.Node;
using Moving.Section;

#nullable enable

namespace Moving
{
    public class PathFactory
    {
        readonly TrainSection.Factory trainFactory;
        readonly StationManager stationManager;

        [Inject]
        PathFactory(TrainSection.Factory trainFactory, StationManager stationManager)
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
                if (node is StationNode sNode)
                {
                    var station = this.stationManager.GetStation(sNode);
                    stations.Add(station);

                    // WalkSectionを作って追加
                    if (walkSectionNodes.Count != 0)
                    {
                        walkSectionNodes.Add(node);
                        var positions = walkSectionNodes.Select(n => new Position(n)).ToList();
                        sections.Add(new WalkSection(positions));

                        walkSectionNodes = new List<INode>();
                    }
                }
                else
                {
                    // TrainSectionを作って追加
                    walkSectionNodes.Add(node);
                    if (stations.Count != 0)
                    {
                        sections.Add(this.trainFactory.Create(stations));
                        stations = new List<Station>();
                    }
                }
            }

            if (walkSectionNodes.Count != 0)
            {
                var positions = walkSectionNodes.Select(n => new Position(n)).ToList();
                sections.Add(new WalkSection(positions));
            }
            else if (stations.Count != 0)
            {
                sections.Add(this.trainFactory.Create(stations));
            }
            else
            {
                // 仮
                throw new System.Exception();
            }

            return new PersonPath(sections, nodes.Last());
        }
    }
}