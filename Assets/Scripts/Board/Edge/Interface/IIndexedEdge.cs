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
        new IIndexedNode From { get; }
        new IIndexedNode To { get; }
    }

    /// <summary>
    /// 型パラメータ付きIIndexedEdge
    /// </summary>
    /// <typeparam name="T">出発点</typeparam>
    /// <typeparam name="U">到着点</typeparam>
    public interface IIndexedEdge<T, U> : IEdge<T, U>, IIndexedEdge
        where T : IIndexedNode
        where U : IIndexedNode
    { }
}