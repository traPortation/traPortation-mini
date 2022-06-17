namespace TraPortation.Traffic.Node
{
    public class TemporaryNode : INode
    {
        public float X { get; }
        public float Y { get; }
        public TemporaryNode(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}