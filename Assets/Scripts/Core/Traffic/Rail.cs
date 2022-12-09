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

        List<Color> railColors = new List<Color>() {
            new Color(1, 0, 0, 1),
            new Color(0, 1, 0, 1),
            new Color(0, 0, 1, 1),
            new Color(1, 1, 0, 1),
            new Color(1, 0, 1, 1),
            new Color(0, 1, 1, 1),
            new Color(0.5f, 0, 0, 1),
            new Color(0, 0.5f, 0, 1),
            new Color(0, 0, 0.5f, 1),
            new Color(0.5f, 0.5f, 0, 1),
            new Color(0.5f, 0, 0.5f, 1),
            new Color(0, 0.5f, 0.5f, 1),
            new Color(0.5f, 0.5f, 0.5f, 1),
            new Color(1, 0.5f, 1, 1),
            new Color(1f, 0.7f, 0.5f, 1f),
            new Color(1f, 0.9f, 0.3f, 1f),
            new Color(0.3f, 0.3f, 0.3f, 1f),
            new Color(1, 0.4f, 0.2f, 1f),
            new Color(0.7f, 0.3f, 0.4f, 1f),
            new Color(0.2f, 0.6f, 0.3f, 1f),
        };

        public Rail(List<Station> stations, int id, string name, IRailView line, TrainPath.Factory factory)
        {
            this.stations = stations;
            this.ID = id;
            this.Name = name;
            this.trains = new List<Train>();

            this.line = line;
            this.line.SetRail(this);
            this.line.SetLine(this.stations.Select(node => new Vector3(node.Node.X, node.Node.Y, 5)).ToArray());

            this.line.SetColor(railColors[id % railColors.Count]);

            this.factory = factory;
        }

        public class Factory : PlaceholderFactory<List<Station>, int, string, Rail> { }

        /// <summary>
        /// 線路上に車両を作成する
        /// </summary>
        public void AddTrain(Train train, Vector3 vec)
        {
            this.trains.Add(train);

            // TODO: IDを設定する
            var path = factory.Create(train.ID, this.stations);

            // 指定した位置に移動させる
            // TODO: 向きの指定
            path.MoveTo(vec);

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