using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    // 片方向の辺
    public class Edge: IEdge
    {
        // それぞれのNode
        public Node From { get; }
        public Node To { get; }

        // edgeの種類 (Const.EdgeType が入る)
        public int Type { get; }

        // 経路探索で用いるコスト
        public float Cost { get; }

        // コンストラクタ
        public Edge(Node from, Node to, float cost, int type) {
            if (cost < 0) throw new System.Exception("cost must not be negative number");
            this.From = from;
            this.To = to;
            this.Cost = cost;
            this.Type = type;
        }
    }
}