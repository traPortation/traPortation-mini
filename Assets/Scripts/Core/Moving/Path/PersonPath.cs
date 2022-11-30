using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace TraPortation.Moving
{
    public class PersonPath : IPath
    {
        public Position Position => this.curSection.Position;
        public SectionStatus Status => this.curSection.Status;

        readonly IReadOnlyList<ISection> sections;
        int index;
        ISection curSection => this.sections[this.index];
        bool finished => this.sections.Last().Status == SectionStatus.Finished;

        public PersonPath(IReadOnlyList<ISection> sections)
        {
            this.sections = sections;
            this.index = 0;
        }

        public void Move(float distance)
        {
            if (this.finished) return;

            // Sectionが始まっていないなら始める
            if (this.curSection.Status == SectionStatus.NotStarted)
            {
                this.curSection.Start();
            }

            // Section内を移動する
            this.curSection.Move(distance);

            // Sectionが終わったら次のSectionに移る
            if (this.curSection.Status == SectionStatus.Finished)
            {
                this.curSection.Dispose();
                if (!this.finished) this.index++;
            }
        }
    }
}