using TraPortation.Traffic.Node;

public class BusStation
{
    public readonly int ID;
    public readonly StationNode Node;
    public BusStation(StationNode node)
    {
        this.Node = node;
        this.ID = node.Index;
    }
}