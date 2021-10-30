﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BoardElements
{
    /// <summary>
    /// (経路探索用に) 複数の辺をまとめて1つの辺として扱う
    /// </summary>
    public class MultiEdge : IEdge
    {
        public INode From { get; }
        public INode To { get; }
        public float Cost { get; }
        private List<IEdge> edges;
        public IReadOnlyList<IEdge> Edges => this.edges;
        public MultiEdge(List<IEdge> edges)
        {
            if (edges.Count == 0) throw new System.Exception("MultiEdge needs 1 Edge at least");
            this.From = edges.First().From;
            this.To = edges.Last().To;
            // それぞれのedgeごとのCostの合計
            this.Cost = edges.Aggregate(0f, (acc, edge) => acc + edge.Cost);
            this.edges = edges;
        }
    }
}