using System.Collections.Generic;
using System.Linq;
using TraPortation.Traffic.Node;

#nullable enable

namespace TraPortation.Moving
{
    public class Path
    {
        public Position Position => this.nowSection.Position;
        public SectionStatus Status => this.nowSection.Status;
        // 必要なさそうなら消す
        public readonly INode LastNode;

        readonly IReadOnlyList<ISection> sections;
        int index;
        ISection nowSection => this.sections[this.index];
        bool finished => this.sections.Last().Status == SectionStatus.Finished;

        public Path(IReadOnlyList<ISection> sections, INode lastNode)
        {
            this.sections = sections;
            this.LastNode = lastNode;
        }

        public void Move(float delta)
        {
            if (this.finished) return;

            // Sectionが始まっていないなら始める
            if (this.nowSection.Status == SectionStatus.NotStarted)
            {
                this.nowSection.Start();
            }

            // Section内を移動する
            this.nowSection.Move(delta);

            // Sectionが終わったら次のSectionに移る
            if (this.nowSection.Status == SectionStatus.Finished)
            {
                this.nowSection.Dispose();
                if (!this.finished) this.index++;
            }
        }
    }
}