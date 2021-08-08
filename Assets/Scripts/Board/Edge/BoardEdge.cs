using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;

namespace BoardElements
{
    /// <summary>
    /// Board上のEdgeを表す
    /// </summary>
    public class BoardEdge : Edge
    {
        public readonly new BoardNode From;
        public readonly new BoardNode To;
        public BoardEdge(BoardNode from, BoardNode to, float cost, Const.EdgeCost.Type type) : base(from, to, cost, type)
        {
            this.From = from;
            this.To = to;
        }
    }
}

