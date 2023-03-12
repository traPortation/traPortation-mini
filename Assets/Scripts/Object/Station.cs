using TraPortation.Traffic.Node;
using TraPortation.UI;

public class Station
{
    public int ID { get; private set; }
    public StationNode Node { get; private set; }
    int peopleCount;
    readonly IStationView view;
    public Station(StationNode node, IStationView view)
    {
        this.ID = node.Index;
        this.Node = node;
        this.view = view;
    }

    public void ChangePeopleCount(int diff)
    {
        lock (this)
        {
            this.peopleCount += diff;
            this.view.SetPeopleCount(this.peopleCount);
        }
    }
}

