using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving;
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
        TrainPath.Factory factory;

        public Rail(List<Station> stations, int id, string name, IRailView line, TrainPath.Factory factory)
        {
            this.stations = stations;
            this.ID = id;
            this.Name = name;
            this.trains = new List<Train>();

            this.line = line;
            this.line.SetRail(this);
            this.line.SetLine(this.stations.Select(node => new Vector3(node.Node.X, node.Node.Y, Const.Z.Rail)).ToArray());

            this.line.SetColor(Const.Color.RailColors[id % Const.Color.RailColors.Count]);

            this.factory = factory;
        }

        public class Factory : PlaceholderFactory<List<Station>, int, string, Rail> { }

        /// <summary>
        /// 線路上に車両を作成する
        /// </summary>
        public void AddTrain(Train train, Vector3 vec, bool direction)
        {
            this.trains.Add(train);

            var path = factory.Create(train.ID, this.stations);

            // 指定した位置に移動させる
            path.MoveTo(vec, direction);

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