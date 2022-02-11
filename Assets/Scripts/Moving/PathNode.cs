using Traffic.Node;
using Traffic.Edge;

#nullable enable

// NOTE: 構造については要検討
public class PathNode
{
    public IIndexedNode Node { get; }
    public IEdge<INode, INode>? Edge { get; }
    public PathNode(IIndexedNode node, IEdge<INode, INode>? edge)
    {
        this.Node = node;
        this.Edge = edge;
    }
}