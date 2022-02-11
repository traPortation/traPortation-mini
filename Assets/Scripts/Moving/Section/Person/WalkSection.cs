using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

#nullable enable

namespace Moving.Section.Person
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

            this.Status = SectionStatus.NotStarted;
            this.Position = new Position(nodes.First());
        }

        public void Start()
        {
            this.Status = SectionStatus.Walking;
        }
        public void Move(float delta)
        {
            if (this.Status != SectionStatus.Walking) return;

            if (this.index >= this.nodes.Count - 1)
            {
                this.Status = SectionStatus.Finished;
                return;
            }

            var dest = this.nodes[this.index + 1];
            var distance = Node.Distance(this.Position, dest);

            if (distance <= delta)
            {
                this.Position = dest;
                this.index++;

                if (this.nodes.Count == this.index + 1)
                {
                    this.Status = SectionStatus.Finished;
                }
            }
            else
            {
                var ratio = delta / distance;
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