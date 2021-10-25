using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// Board上のEdgeを表す
    /// </summary>
    public class VehicleEdge : IEdge
    {
        readonly StationNode from;
        public INode From => this.from;
        readonly StationNode to;
        public INode To => this.to;
        public float Cost { get; }
        public VehicleEdge(StationNode from, StationNode to, float cost)
        {
            this.from = from;
            this.to = to;
            this.Cost = cost;
        }
    }
}

