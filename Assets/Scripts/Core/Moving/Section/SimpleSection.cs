using System.Collections.Generic;
using UnityEngine;

#nullable enable
namespace TraPortation.Moving.Section
{
    public class SimpleSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        IReadOnlyList<Position> positions;
        int index;
        public SimpleSection(IReadOnlyList<Position> positions)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = positions[0];
            this.positions = positions;
            this.Rotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), this.positions[this.index + 1].ToVector3(0) - this.Position.ToVector3(0));
            this.index = 0;
        }
        public void Start()
        {
            this.Status = SectionStatus.Moving;
        }
        public void Move(float distance)
        {
            var next = this.positions[this.index + 1];
            var nextDist = Position.Distance(this.Position, next);

            // 次の地点に着く場合
            if (nextDist <= distance)
            {
                this.Position = next;
                this.index++;

                if (this.index == this.positions.Count - 1)
                {
                    this.Status = SectionStatus.Finished;
                }
                else
                {
                    this.Rotation = Quaternion.FromToRotation(new Vector3(-1, 0, 0), this.positions[this.index + 1].ToVector3(0) - this.Position.ToVector3(0));
                }
            }
            // 着かない場合
            else
            {
                var ratio = distance / nextDist;
                var x = this.Position.X + (next.X - this.Position.X) * ratio;
                var y = this.Position.Y + (next.Y - this.Position.Y) * ratio;
                this.Position = new Position(x, y);
            }
        }

        public void Dispose() { }
    }
}