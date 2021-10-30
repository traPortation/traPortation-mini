using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class RailManager
{
    /// <summary>
    /// 路線を作成する
    /// </summary>
    
    public int Index { get; }

    List<Rail> rails { get; }
    public IReadOnlyList<Rail> Rails => this.rails;

    public RailManager()
    {
        this.rails = new List<Rail>();
    }

    public void AddRail(List<VehicleEdge> edges)
    {
        int index = this.rails[this.rails.Count].Index;
        var rail = new Rail(edges, index);

        this.rails.Add(rail);
    }
}
