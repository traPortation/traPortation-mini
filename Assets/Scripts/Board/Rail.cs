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

    public int Index { get; }

    // TODO: そのうちVehiclePathNodeみたいなの作ったらそれにする
    List<PathNode> nodes { get; }
    public IReadOnlyList<PathNode> Nodes => this.nodes;

    List<Train> trains { get; }
    public IReadOnlyList<Train> Trains => this.trains;

    public Rail(List<PathNode> nodes, int index)
    {
        this.nodes = nodes;
        this.Index = index;
        this.trains = new List<Train>();
    }

    public void AddTrain(Train train)
    {
        this.trains.Add(train);
        var path = new Path(nodes, train.transform);
        train.Initialize(path);
    }
}
