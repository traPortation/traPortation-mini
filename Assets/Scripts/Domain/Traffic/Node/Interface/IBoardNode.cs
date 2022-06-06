using System.Collections.Generic;
using TraPortation.Traffic.Edge;

namespace TraPortation.Traffic.Node
{
    /// <summary>
    /// Board上のNode
    /// </summary>
    public interface IBoardNode : IIndexedNode
    {
        IEnumerable<IEdge<IBoardNode, IBoardNode>> Edges { get; }
    }
}
