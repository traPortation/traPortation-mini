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

    List<VehicleEdge> edges { get; }
    public IReadOnlyList<VehicleEdge> Edges => this.edges;

    List<Train> trains { get; }
    public IReadOnlyList<Train> Trains => this.trains;

    public Rail(List<VehicleEdge> edges, int index)
    {
        this.edges = edges;
        this.Index = index;
        this.trains = new List<Train>();
    }
    
    public void AddTrain(Train train)
    {
        this.trains.Add(train);
        var path = new Path(edges.ToList<IEdge>());
        train.Initialize(path);
    }
}
