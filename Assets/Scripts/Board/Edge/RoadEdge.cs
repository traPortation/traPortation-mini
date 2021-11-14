using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 道
    /// </summary>
    /// <typeparam name="T">到着点</typeparam>
    public class RoadEdge<T> : IIndexedEdge<T>, IIndexedEdge where T : IBoardNode
    {
        public T To { get; }
        INode IEdge.To => this.To;
        IIndexedNode IIndexedEdge.To => this.To;

        public float Cost { get; }
        public RoadEdge(T to, float cost)
        {
            this.To = to;
            this.Cost = cost;
        }
    }
}
