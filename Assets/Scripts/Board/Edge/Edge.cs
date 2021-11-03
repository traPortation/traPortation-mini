using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// シンプルなEdge
    /// 現在は経路探索でのみ使用
    /// </summary>
    public class Edge : IEdge
    {
        public INode From { get; }
        public INode To { get; }
        public float Cost { get; }
        public Edge(INode from, INode to, float cost, Const.EdgeType type)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }
    }
}