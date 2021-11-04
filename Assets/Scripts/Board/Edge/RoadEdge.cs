using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 道
    /// </summary>
    /// <typeparam name="T">出発点</typeparam>
    /// <typeparam name="U">到着点</typeparam>
    public class RoadEdge<T, U> : IIndexedEdge<T, U>, IIndexedEdge
        where T : IBoardNode
        where U : IBoardNode
    {
        public T From { get; }
        public U To { get; }
        INode IEdge.From => this.From;
        INode IEdge.To => this.To;
        IIndexedNode IIndexedEdge.From => this.From;
        IIndexedNode IIndexedEdge.To => this.To;

        public float Cost { get; }
        public RoadEdge(T from, U to, float cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}
