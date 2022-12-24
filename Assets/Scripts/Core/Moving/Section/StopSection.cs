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
        int milliSeconds;
        public StopSection(Position position, int milliSeconds)
        {
            this.Status = SectionStatus.NotStarted;
            this.Position = position;
            this.Rotation = Quaternion.identity;
			this.milliSeconds = milliSeconds;
        }
        public void Start()
        {
            stop();
        }
        public void Move(float distance) { }

        async void stop()
        {
			this.Status = SectionStatus.Stopping;
            await UniTask.Delay(this.milliSeconds);
            this.Status = SectionStatus.Finished;
        }

        public void Dispose() { }
    }
}