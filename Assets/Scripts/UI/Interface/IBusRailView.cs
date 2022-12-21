using TraPortation.Traffic;

namespace TraPortation.UI
{
    public interface IBusRailView : ILine
    {
        void SetRail(BusRail rail);
    }
}