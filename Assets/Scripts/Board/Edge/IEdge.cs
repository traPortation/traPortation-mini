using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    public interface IEdge
    {
        INode From { get; }
        INode To { get; }
        float Cost { get; }
    }
}