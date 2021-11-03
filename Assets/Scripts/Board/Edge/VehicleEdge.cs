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
    public class VehicleEdge : IIndexedEdge<StationNode, StationNode>, IIndexedEdge
    {
        public StationNode From { get; }
        public StationNode To { get; }
        INode IEdge.From => this.From;
        INode IEdge.To => this.To;
        IIndexedNode IIndexedEdge.From => this.From;
        IIndexedNode IIndexedEdge.To => this.To;
        public float Cost { get; }
        public VehicleEdge(StationNode from, StationNode to, float cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}

