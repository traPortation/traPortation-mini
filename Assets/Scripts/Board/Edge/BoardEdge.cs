using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    public class BoardEdge : Edge {
        public new BoardNode From { get; }
        public new BoardNode To { get; }
        public BoardEdge(BoardNode from, BoardNode to, float cost, int type): base(from, to, cost, type) {
            this.From = from;
            this.To = to;
        }
    }
}

