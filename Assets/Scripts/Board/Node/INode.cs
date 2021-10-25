using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 頂点
    /// </summary>
    public interface INode
    {
        float X { get; }
        float Y { get; }
        int Index { get; }
        IReadOnlyList<IEdge> Edges { get; }
        void AddEdge(IEdge edge);
    }
}