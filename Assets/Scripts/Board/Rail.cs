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

    List<VehicleEdge> edges { get; }
    public IReadOnlyList<VehicleEdge> Edges => this.edges;

    List<Train> trains { get; }
    public IReadOnlyList<Train> Trains => this.trains;

    public Rail(List<VehicleEdge> edges, int id, string name)
    {
        this.edges = edges;
        this.ID = id;
        this.Name = name;
        this.trains = new List<Train>();
    }
    
    public void AddTrain(Train train)
    {
        this.trains.Add(train);
        var path = new Path(edges.ToList<IIndexedEdge>());
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
