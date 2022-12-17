using System.Collections.Generic;
using TraPortation.Traffic;
using UnityEngine;
using Zenject;

namespace TraPortation.UI
{
    public class RailView : IRailView
    {
        List<RailLine> lines;
        Color color;
        public Rail Rail { get; private set; }

        [Inject]
        public RailView()
        {
            this.lines = new List<RailLine>();
            this.color = Color.red;
        }

        public void SetRail(Rail rail)
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
                obj.name = "RailView";
                var line = obj.AddComponent<RailLine>();
                line.Construct();
                line.SetLine(new Vector3[] { positions[i], positions[i + 1] });
                line.SetColor(this.color);
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