using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 両端点がインデックスを持っているような辺
    /// </summary>
    public interface IIndexedEdge: IEdge
    {
        new IIndexedNode From { get; }
        new IIndexedNode To { get; }
    }
}