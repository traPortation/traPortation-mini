using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

public class Rail
{
    /// <summary>
    /// 線路上に車両を作成する
    /// </summary>

    public int ID { get; }

    public string Name { get; private set; }

    // TODO: そのうちVehiclePathNodeみたいなの作ったらそれにする
    List<PathNode> nodes { get; }
    public IReadOnlyList<PathNode> Nodes => this.nodes;

    List<Train> trains { get; }
    public IReadOnlyList<Train> Trains => this.trains;

    public Rail(List<PathNode> nodes, int id, string name)
    {
        this.nodes = nodes;
        this.ID = id;
        this.Name = name;
        this.trains = new List<Train>();
    }

    public void AddTrain(Train train)
    {
        this.trains.Add(train);
        var path = new Path(nodes, train.transform);
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
