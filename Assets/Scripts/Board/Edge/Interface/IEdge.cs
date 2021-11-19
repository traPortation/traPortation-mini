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
        INode To { get; }
        float Cost { get; }
    }

    /// <summary>
    /// 型パラメータ付きIEdge
    /// </summary>
    /// <typeparam name="T">到着点</typeparam>
    public interface IEdge<out T> : IEdge where T : INode
    {
        // newしてるのはそれしか方法が思い浮かばなかったから
        new T To { get; }
    }

}