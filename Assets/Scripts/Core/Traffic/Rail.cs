using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving;
using TraPortation.Moving.Section.Train;
using TraPortation.UI;
using UnityEngine;
using Zenject;

namespace TraPortation.Traffic
{
    public class Rail
    {
        public int ID { get; }

        public string Name { get; private set; }

        List<Station> stations { get; }
        public IReadOnlyList<Station> Stations => this.stations;

        List<Train> trains { get; }
        public IReadOnlyList<Train> Trains => this.trains;
        IRailView line;
        TrainSection.Factory factory;

        public Rail(List<Station> stations, int id, string name, IRailView line, TrainSection.Factory factory)
        {
            this.stations = stations;
            this.ID = id;
            this.Name = name;
            this.trains = new List<Train>();

            this.line = line;
            this.line.SetLine(this.stations.Select(node => new Vector3(node.Node.X, node.Node.Y, 0)).ToArray());

            // TODO: 路線ごとに変える
            this.line.SetColor(Color.red);

            this.factory = factory;
        }

        public class Factory : PlaceholderFactory<List<Station>, int, string, Rail> { }

        /// <summary>
        /// 線路上に車両を作成する
        /// </summary>
        public void AddTrain(Train train)
        {
            this.trains.Add(train);

            var sections = new List<ISection>();
            sections.Add(factory.Create(this.stations, train.ID, TraPortation.Const.Train.StopStationTime));
            var path = new Path(sections, this.stations.Last().Node);

            train.Initialize(path);
        }

        public void ChangeRailName(Rail rail)
        {
            var railName = new inputName();
            if (railName.resultName != "")
            {
                rail.Name = railName.resultName;
            }
        }
    }
}