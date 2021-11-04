using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// Board上のNode
    /// </summary>
    public interface IBoardNode : IIndexedNode
    {
        IReadOnlyList<IEdge<IBoardNode, IBoardNode>> Edges { get; }
    }
}
