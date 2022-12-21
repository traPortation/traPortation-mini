using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TraPortation.Core.RoadGen
{
    public class Road
    {
        public readonly Line line;
        List<(Vector2, Road)> neigbors;

        public Road(Line line)
        {
            this.line = line;
            this.neigbors = new List<(Vector2, Road)>();
        }

        public void AddNeigbor(Vector2 point, Road road)
        {
            this.neigbors.Add((point, road));
        }

        public IReadOnlyList<(Vector2, Road)> GetNeigbors() {
            this.neigbors.Sort((a, b) =>
            {
                var (v1, _) = a;
                var (v2, _) = b;
                return (v1 - this.line.start).magnitude.CompareTo((v2 - this.line.start).magnitude);
            });

            return this.neigbors;
        }
    }
}