using System.Collections.Generic;
using TraPortation.Moving;

namespace TraPortation.Traffic
{
    public class BusRoute
    {
        public readonly BusStation Start;
        public readonly BusStation End;
        public readonly IReadOnlyList<Position> Positions;

        public BusRoute(BusStation start, BusStation end, IReadOnlyList<Position> positions)
        {
            this.Start = start;
            this.End = end;
            this.Positions = positions;
        }

        public float Distance()
        {
            float distance = 0;
            for (int i = 0; i < this.Positions.Count - 1; i++)
            {
                distance += Position.Distance(this.Positions[i], this.Positions[i + 1]);
            }
            return distance;
        }
    }
}