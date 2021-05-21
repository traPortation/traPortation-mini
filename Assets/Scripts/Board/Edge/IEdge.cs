using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardElements {
    public interface IEdge {
        Node From { get; }
        Node To { get; }
        float Cost { get; }
    }
}