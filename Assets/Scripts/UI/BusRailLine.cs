using TraPortation.Traffic;

namespace TraPortation.UI
{
    public class BusRailLine : Line
    {
        public BusRail Rail { get; private set; }

        public void SetRail(BusRail rail)
        {
            this.Rail = rail;
        }
    }
}