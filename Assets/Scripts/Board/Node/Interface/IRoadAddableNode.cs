using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// 道の追加ができる辺
    /// </summary>
    /// <typeparam name="T">自分自身</typeparam>
    public interface IRoadAddableNode: INode
    {
        RoadEdge<IBoardNode> AddRoad(IBoardNode toNode, float cost);
    }
}
