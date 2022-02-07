using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Rail
{
    public int ID { get; }

    public string Name { get; private set; }

    // TODO: そのうちVehiclePathNodeみたいなの作ったらそれにする
    List<PathNode> nodes { get; }
    public IReadOnlyList<PathNode> Nodes => this.nodes;

    List<Train> trains { get; }
    public IReadOnlyList<Train> Trains => this.trains;
    UI.ILine line;

    Rail(List<PathNode> nodes, int id, string name, UI.ILine line)
    {
        this.nodes = nodes;
        this.ID = id;
        this.Name = name;
        this.trains = new List<Train>();

        this.line = line;
        this.line.SetLine(this.nodes.Select(node => new Vector3(node.Node.X, node.Node.Y, 0)).ToArray());

        // TODO: 路線ごとに変える
        this.line.SetColor(Color.red);
    }

    public class Factory : PlaceholderFactory<List<PathNode>, int, string, Rail> { }

    /// <summary>
    /// 線路上に車両を作成する
    /// </summary>
    public void AddTrain(Train train)
    {
        this.trains.Add(train);
        var path = new Path(nodes);
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
