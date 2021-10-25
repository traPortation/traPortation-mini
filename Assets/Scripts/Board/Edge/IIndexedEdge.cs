using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 両端点がインデックスを持っているような辺
    /// </summary>
    
    // IEdgeをオーバーライドしているので明示的なインターフェースの実装が必要
    public interface IIndexedEdge: IEdge
    {
        new IIndexedNode From { get; }
        new IIndexedNode To { get; }
    }
}