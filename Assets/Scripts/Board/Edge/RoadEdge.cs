using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    public class RoadEdge : IIndexedEdge
    {
        public IIndexedNode From { get; }
        INode IEdge.From => this.From;
        IIndexedNode IIndexedEdge.From => this.From;

        public IIndexedNode To { get; }
        INode IEdge.To => this.To;
        IIndexedNode IIndexedEdge.To => this.To;

        public float Cost { get; }
        public RoadEdge(IIndexedNode from, IIndexedNode to, float cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}