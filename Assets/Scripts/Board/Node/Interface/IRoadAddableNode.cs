using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{

    public interface IRoadAddableNode<T> : IBoardNode where T : IRoadAddableNode<T>
    {
        RoadEdge<T, IBoardNode> AddRoad(IBoardNode toNode, float cost);
    }
}
