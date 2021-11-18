using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 両端点がインデックスを持っているような辺
    /// </summary>
    public interface IIndexedEdge : IEdge
    {
        new IIndexedNode To { get; }
    }

    /// <summary>
    /// 型パラメータ付きIIndexedEdge
    /// </summary>
    /// <typeparam name="T">到着点</typeparam>
    public interface IIndexedEdge<T> : IEdge<T>, IIndexedEdge
        where T : IIndexedNode
    { }
}