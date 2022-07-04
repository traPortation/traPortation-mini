using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace TraPortation.Moving.Section.Person
{
    public class WalkSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; private set; }
        int index;
        readonly IReadOnlyList<Position> nodes;

        public WalkSection(IReadOnlyList<Position> nodes)
        {
            this.nodes = nodes;

            if (this.nodes.Count <= 1)
            {
                throw new System.ArgumentException();
            }

            this.Status = SectionStatus.NotStarted;
            this.Position = nodes.First();
        }

        public void Start()
        {
            this.Status = SectionStatus.Walking;
        }
        public void Move(float distance)
        {
            if (this.Status != SectionStatus.Walking) return;

            var dest = this.nodes[this.index + 1];
            var goalDistance = Position.Distance(this.Position, dest);

            // 次の目的地に着く場合
            if (goalDistance <= distance)
            {
                this.Position = dest;
                this.index++;

                if (this.index == this.nodes.Count - 1)
                {
                    this.Status = SectionStatus.Finished;
                }
            }
            // 着かない場合
            else
            {
                var ratio = distance / goalDistance;
                var x = this.Position.X + (dest.X - this.Position.X) * ratio;
                var y = this.Position.Y + (dest.Y - this.Position.Y) * ratio;
                this.Position = new Position(x, y);
            }
        }

        public void Dispose()
        {
        }
    }
}