using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 出発点と到着点とコストを持つ辺
    /// </summary>
    public interface IEdge
    {
        INode From { get; }
        INode To { get; }
        float Cost { get; }
    }
}