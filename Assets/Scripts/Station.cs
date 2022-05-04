using Traffic.Node;

public class Station
{
    public int ID { get; private set; }
    public StationNode Node { get; private set; }
    public Station(StationNode node)
    {
        this.ID = node.Index;
        this.Node = node;
    }
}
