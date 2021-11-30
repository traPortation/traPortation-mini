using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// 区画に出入りするための辺
    /// </summary>
    /// <typeparam name="T">到着点</typeparam>
    public class PlotEdge<T> : IIndexedEdge<T>, IIndexedEdge where T : IIndexedNode
    {
        public T To { get; }
        INode IEdge.To => this.To;
        IIndexedNode IIndexedEdge.To => this.To;

        public float Cost { get; }
        public PlotEdge(T to, float cost)
        {
            this.To = to;
            this.Cost = cost;
        }
    }
}
