using System.Collections.Generic;
using TraPortation.Traffic;
using UnityEngine;
using Zenject;


namespace TraPortation.UI
{
    // NOTE: 実装の省略のためにRailViewをそのまま使っている
    public class BusRailView : IBusRailView
    {
        List<BusRailLine> lines;
        Color color;
        public BusRail Rail { get; private set; }

        [Inject]
        public BusRailView()
        {
            this.lines = new List<BusRailLine>();
            this.color = Color.red;
        }

        public void SetRail(BusRail rail)
        {
            this.Rail = rail;
            foreach (var line in this.lines)
            {
                line.SetRail(rail);
            }
        }

        public void SetLine(Vector3[] positions)
        {
            for (int i = 0; i < positions.Length - 1; i++)
            {
                GameObject obj = new GameObject();
                obj.name = "BusRailView";
                var line = obj.AddComponent<BusRailLine>();
                line.Construct();
                line.SetLine(new Vector3[] { positions[i], positions[i + 1] });
                line.SetColor(this.color);
                line.SetZ(1.0f);
                line.SetRail(this.Rail);
                lines.Add(line);
            }
        }
        public void SetColor(Color color)
        {
            this.color = color;
            foreach (var line in lines)
            {
                line.SetColor(color);
            }
        }
    }
}