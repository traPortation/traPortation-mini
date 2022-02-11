using System.Collections.Generic;
using System.Linq;
using Traffic.Node;

#nullable enable

namespace Moving
{
    public class PersonPath
    {
        readonly IReadOnlyList<ISection> sections;
        int index;
        public Position Position => this.sections[this.index].Position;
        public SectionStatus Status => this.sections[this.index].Status;
        bool finished => this.sections.Last().Status == SectionStatus.Finished;
        // あとで消す
        public readonly INode LastNode;
        public PersonPath(IReadOnlyList<ISection> sections, INode lastNode)
        {
            this.sections = sections;
            this.LastNode = lastNode;
        }
        public void Move(float delta)
        {
            if (this.finished) return;

            var section = this.sections[this.index];

            if (section.Status == SectionStatus.NotStarted)
            {
                section.Start();
            }

            section.Move(delta);
            if (section.Status == SectionStatus.Finished)
            {
                this.sections[this.index].Dispose();
                if (!this.finished) this.index++;
            }
        }
    }
}