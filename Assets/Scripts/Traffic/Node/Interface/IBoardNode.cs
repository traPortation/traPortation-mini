using System.Collections.Generic;
using Traffic.Edge;

namespace Traffic.Node
{
    /// <summary>
    /// Board上のNode
    /// </summary>
    public interface IBoardNode : IIndexedNode
    {
        IEnumerable<IEdge<IBoardNode, IBoardNode>> Edges { get; }
    }
}
