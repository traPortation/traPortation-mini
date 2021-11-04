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

    /// <summary>
    /// 型パラメータ付きIEdge
    /// </summary>
    /// <typeparam name="T">出発点</typeparam>
    /// <typeparam name="U">到着点</typeparam>
    public interface IEdge<out T, out U> : IEdge where T : INode where U : INode
    {
        // newしてるのはそれしか方法が思い浮かばなかったから
        new T From { get; }
        new U To { get; }
    }

}