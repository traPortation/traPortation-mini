using System.Collections.Generic;
using System.Linq;
using TraPortation.Moving.Section;
using UnityEngine;

#nullable enable

namespace TraPortation.Moving
{
    public class PersonPath : IPath
    {
        public Position Position => this.curSection.Position;
        public Quaternion Rotation => this.curSection.Rotation;
        public SectionStatus Status => this.curSection.Status;

        readonly IReadOnlyList<ISection> sections;
        int index;
        ISection curSection;
        bool finished => this.sections.Last().Status == SectionStatus.Finished;

        public PersonPath(IReadOnlyList<ISection> sections)
        {
            this.sections = sections;
            this.curSection = this.sections[0];
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
                if (!this.finished)
                {
                    this.index++;
                    this.curSection = this.sections[this.index];
                }
            }
        }
    }
}