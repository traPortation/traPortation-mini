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
    public class PlotEdge<T, U> : IEdge<T, U> where T : INode where U: INode
    {
        public T From { get; }
        public U To { get; }

        public float Cost { get; }
        public PlotEdge(T from, U to, float cost)
        {
            this.To = to;
            this.Cost = cost;
        }
    }
}
