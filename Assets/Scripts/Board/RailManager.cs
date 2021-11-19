using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

public class RailManager : Singleton<RailManager>
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

    public void AddRail(List<PathNode> edges)
    {
        int index = this.rails[this.rails.Count-1].ID+1;
        string indexName = index.ToString();
        var rail = new Rail(edges, index, $"Rail {indexName}");

        this.rails.Add(rail);
    }


}
