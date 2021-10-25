﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    /// <summary>
    /// ただのNode
    /// 将来的に必要なくなるかも
    /// </summary>
    class Node : INode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        List<IEdge> edges { get; }
        public IReadOnlyList<IEdge> Edges => this.edges;
        public Node(float x, float y)
        {

            this.X = x;
            this.Y = y;
            this.edges = new List<IEdge>();
        }
        public void AddEdge(IEdge edge)
        {
            this.edges.Add(edge);
        }
    }
}