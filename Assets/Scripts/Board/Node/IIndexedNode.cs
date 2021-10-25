using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// インデックス付き頂点
    /// インデックスはBoardで管理される
    /// </summary>
    public interface IIndexedNode : INode
    {
        int Index { get; }
        IReadOnlyList<IIndexedEdge> Edges { get; }
        void AddEdge(IIndexedEdge edge);
    }
}