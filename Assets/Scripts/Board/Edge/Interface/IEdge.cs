using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements
{
    public interface IEdge<out T, out U> where T : INode
    {
        T From { get; }
        U To { get; }
        float Cost { get; }
    }

}