using System.Collections;
using System.Collections.Generic;
using Zenject;
using Moving;

public class RailManager
{
    List<Rail> rails { get; }
    public IReadOnlyList<Rail> Rails => this.rails;
    Rail.Factory railFactory;

    [Inject]
    RailManager(Rail.Factory factory)
    {
        this.railFactory = factory;
        this.rails = new List<Rail>();
    }

    /// <summary>
    /// 路線を作成する
    /// </summary>
    public Rail AddRail(List<PathNode> edges)
    {
        int index = 0;
        if (this.rails.Count != 0)
        {
            index = this.rails[this.rails.Count - 1].ID + 1;
        }

        string indexName = index.ToString();

        var rail = this.railFactory.Create(edges, index, $"Rail {indexName}");

        this.rails.Add(rail);

        return rail;
    }
}
