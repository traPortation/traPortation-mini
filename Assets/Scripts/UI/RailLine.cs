using TraPortation.Traffic;

namespace TraPortation.UI
{
    public class RailLine : Line
    {
		public Rail Rail { get; private set; }

		public void SetRail(Rail rail)
		{
			this.Rail = rail;
		}
    }
}