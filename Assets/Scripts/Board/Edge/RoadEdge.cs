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
    public class RoadEdge : IEdge<IBoardNode, IBoardNode>
    {
        public IBoardNode From { get; }
        public IBoardNode To { get; }
        public float Cost { get; }
        public RoadEdge(IBoardNode from, IBoardNode to, float cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}
