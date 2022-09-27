using TraPortation.Traffic;

namespace TraPortation.UI
{
    public interface IRailView : ILine
    {
        Rail Rail { get; }
        void SetRail(Rail rail);
    }
}