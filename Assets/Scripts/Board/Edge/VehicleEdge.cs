using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 乗り物が通る辺
    /// </summary>

    // 乗り物の種類が増えるのに合わせて抽象クラスにするなりInterfaceにするなりするかも
    public class VehicleEdge : IEdge<StationNode, StationNode>
    {
        public StationNode From { get; }
        public StationNode To { get; }
        public float Cost { get; }
        public VehicleEdge(StationNode to, float cost)
        {
            this.To = to;
            this.Cost = cost;
        }
    }
}

