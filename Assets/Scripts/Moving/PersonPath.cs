using System.Collections.Generic;
using System.Linq;
using Traffic.Node;

#nullable enable

namespace Moving
{
    public class PersonPath
    {
        public Position Position => this.nowSection.Position;
        public SectionStatus Status => this.nowSection.Status;
        readonly IReadOnlyList<ISection> sections;
        int index;
        ISection nowSection => this.sections[this.index];
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

            if (this.nowSection.Status == SectionStatus.NotStarted)
            {
                this.nowSection.Start();
            }

            this.nowSection.Move(delta);
            if (this.nowSection.Status == SectionStatus.Finished)
            {
                this.nowSection.Dispose();
                if (!this.finished) this.index++;
            }
        }
    }
}