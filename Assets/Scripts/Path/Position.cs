using Traffic.Node;

public struct Position : INode
{
    public float X { get; set; }
    public float Y { get; set; }
    public Position(INode node)
    {
        this.X = node.X;
        this.Y = node.Y;
    }
    public Position(float x, float y)
    {
        this.X = x;
        this.Y = y;
    }
}