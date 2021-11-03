using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// インデックス付き頂点 (駅や交差点など)
    /// インデックスはBoardで管理される
    /// </summary>
    public interface IIndexedNode : INode
    {
        int Index { get; }
    }
}