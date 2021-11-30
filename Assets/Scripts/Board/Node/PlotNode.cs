using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardElements;
using System.Linq;

namespace BoardElements
{
    /// <summary>
    /// 土地区画のNode
    /// </summary>
    public class PlotNode : INode, IIndexedNode
    {
        public float X { get; }
        public float Y { get; }
        public int Index { get; }
        public PlotNode(float x, float y, int idx)
        {
            this.X = x;
            this.Y = y;
            this.Index = idx;
        }
    }
}


