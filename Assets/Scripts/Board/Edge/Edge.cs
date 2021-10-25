using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// シンプルなEdge
    /// </summary>
    public class Edge : IEdge
    {
 
        public INode From { get; }
        public INode To { get; }
        public float Cost { get; }
        public Edge(INode from, INode to, float cost, Const.EdgeCost.Type type)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}