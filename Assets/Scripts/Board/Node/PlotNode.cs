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
    public class PlotNode : INode
    {
        public float X { get; }
        public float Y { get; }
        public PlotNode(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}


