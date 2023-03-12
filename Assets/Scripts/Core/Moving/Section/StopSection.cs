using Cysharp.Threading.Tasks;
using UnityEngine;

#nullable enable

namespace TraPortation.Moving.Section
{
    public class StopSection : ISection
    {
        public SectionStatus Status { get; private set; }
        public Position Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        int frame;
        int count;

        public StopSection(Position position, int milliSeconds)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = position;
            this.Rotation = Quaternion.identity;
            this.frame = (int)(milliSeconds / 1000f * 60f);
        }
        public void Start()
        {
            this.Status = SectionStatus.Stopping;
        }
        public void Move(float distance)
        {
            this.count++;
            if (this.count >= this.frame)
            {
                this.Status = SectionStatus.Finished;
            }
        }

        public void Dispose() { }
    }
}