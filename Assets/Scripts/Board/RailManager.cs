using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class RailManager
{
    /// <summary>
    /// 路線を作成する
    /// </summary>
    
    List<Rail> rails { get; }
    public IReadOnlyList<Rail> Rails => this.rails;

    public RailManager()
    {
        this.rails = new List<Rail>();
    }

    public void AddRail(List<VehicleEdge> edges)
    {
        int index = this.rails[this.rails.Count].Index;
        string indexName = index.ToString();
        var rail = new Rail(edges, index, $"Rail {indexName}");

        this.rails.Add(rail);
    }

    public void ChangeRailName(Rail rail)
    {
        if (Name.resultName != "") {
            rail.Name = Name.resultName;
        }
    }
}
